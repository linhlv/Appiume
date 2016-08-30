using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using Appiume.Apm.Aspects;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.Threading;
using Appiume.Apm.Timing;
using Castle.Core.Logging;
using Castle.DynamicProxy;

namespace Appiume.Apm.Auditing
{
    internal class AuditingInterceptor : IInterceptor
    {
        public IApmSession ApmSession { get; set; }

        public ILogger Logger { get; set; }

        public IAuditingStore AuditingStore { get; set; }

        private readonly IAuditingConfiguration _configuration;

        private readonly IAuditInfoProvider _auditInfoProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AuditingInterceptor(IAuditingConfiguration configuration, IAuditInfoProvider auditInfoProvider, IUnitOfWorkManager unitOfWorkManager)
        {
            _configuration = configuration;
            _auditInfoProvider = auditInfoProvider;
            _unitOfWorkManager = unitOfWorkManager;

            ApmSession = NullApmSession.Instance;
            Logger = NullLogger.Instance;
            AuditingStore = SimpleLogAuditingStore.Instance;
        }

        public void Intercept(IInvocation invocation)
        {
            if (ApmCrossCuttingConcerns.IsApplied(invocation.InvocationTarget, ApmCrossCuttingConcerns.Auditing))
            {
                invocation.Proceed();
                return;
            }

            if (!AuditingHelper.ShouldSaveAudit(invocation.MethodInvocationTarget, _configuration, ApmSession))
            {
                invocation.Proceed();
                return;
            }

            var auditInfo = CreateAuditInfo(invocation);

            if (AsyncHelper.IsAsyncMethod(invocation.Method))
            {
                PerformAsyncAuditing(invocation, auditInfo);
            }
            else
            {
                PerformSyncAuditing(invocation, auditInfo);
            }
        }

        private AuditInfo CreateAuditInfo(IInvocation invocation)
        {
            var auditInfo = new AuditInfo
            {
                TenantId = ApmSession.TenantId,
                UserId = ApmSession.UserId,
                ImpersonatorUserId = ApmSession.ImpersonatorUserId,
                ImpersonatorTenantId = ApmSession.ImpersonatorTenantId,
                ServiceName = invocation.MethodInvocationTarget.DeclaringType != null
                    ? invocation.MethodInvocationTarget.DeclaringType.FullName
                    : "",
                MethodName = invocation.MethodInvocationTarget.Name,
                Parameters = ConvertArgumentsToJson(invocation),
                ExecutionTime = Clock.Now
            };

            _auditInfoProvider.Fill(auditInfo);

            return auditInfo;
        }

        private void PerformSyncAuditing(IInvocation invocation, AuditInfo auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                AuditingStore.Save(auditInfo);
            }
        }
        private void PerformAsyncAuditing(IInvocation invocation, AuditInfo auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithFinally(
                    (Task)invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, stopwatch, exception)
                    );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, stopwatch, exception)
                    );
            }
        }

        private string ConvertArgumentsToJson(IInvocation invocation)
        {
            try
            {
                var parameters = invocation.MethodInvocationTarget.GetParameters();
                if (parameters.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();
                for (int i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var argument = invocation.Arguments[i];
                    dictionary[parameter.Name] = argument;
                }

                return AuditingHelper.Serialize(dictionary);
            }
            catch (Exception ex)
            {
                Logger.Warn("Could not serialize arguments for method: " + invocation.MethodInvocationTarget.Name);
                Logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }

        private void SaveAuditInfo(AuditInfo auditInfo, Stopwatch stopwatch, Exception exception)
        {
            stopwatch.Stop();
            auditInfo.Exception = exception;
            auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                AuditingStore.Save(auditInfo);
                uow.Complete();
            }
        }
    }
}