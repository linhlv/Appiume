using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Appiume.Apm.Aspects;
using Appiume.Apm.Auditing;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Dependency;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.Timing;
using Appiume.Apm.Web.WebApi.Validation;
using Castle.Core.Logging;

namespace Appiume.Apm.Web.WebApi.Auditing
{
    public class ApmApiAuditFilter : IActionFilter, ITransientDependency
    {
        /// <summary>
        /// Ignored types for serialization on audit logging.
        /// </summary>
        public static List<Type> IgnoredTypesForSerializationOnAuditLogging { get; }

        public bool AllowMultiple => false;

        public IAuditInfoProvider AuditInfoProvider { get; set; }

        public IAuditingStore AuditingStore { get; set; }

        public IApmSession ApmSession { get; set; }

        public ILogger Logger { get; set; }

        private readonly IAuditingConfiguration _auditingConfiguration;

        static ApmApiAuditFilter()
        {
            IgnoredTypesForSerializationOnAuditLogging = new List<Type>();
        }

        public ApmApiAuditFilter(IAuditingConfiguration auditingConfiguration)
        {
            _auditingConfiguration = auditingConfiguration;

            ApmSession = NullApmSession.Instance;
            AuditingStore = SimpleLogAuditingStore.Instance;
            AuditInfoProvider = NullAuditInfoProvider.Instance;
            Logger = NullLogger.Instance;
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.ActionDescriptor.GetMethodInfoOrNull() == null ||
                !ShouldSaveAudit(actionContext))
            {
                return await continuation();
            }

            var auditInfo = CreateAuditInfo(actionContext);
            var stopwatch = Stopwatch.StartNew();

            try
            {
                return await continuation();
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
                AuditInfoProvider?.Fill(auditInfo);
                await AuditingStore.SaveAsync(auditInfo);
            }
        }

        private AuditInfo CreateAuditInfo(HttpActionContext context)
        {
            var auditInfo = new AuditInfo
            {
                TenantId = ApmSession.TenantId,
                UserId = ApmSession.UserId,
                ImpersonatorUserId = ApmSession.ImpersonatorUserId,
                ImpersonatorTenantId = ApmSession.ImpersonatorTenantId,
                ServiceName = context.ControllerContext.Controller?.GetType().ToString() ?? "",
                MethodName = context.ActionDescriptor.ActionName,
                Parameters = ConvertArgumentsToJson(context.ActionArguments),
                ExecutionTime = Clock.Now
            };

            AuditInfoProvider.Fill(auditInfo);

            return auditInfo;
        }

        private bool ShouldSaveAudit(HttpActionContext context)
        {
            if (!_auditingConfiguration.IsEnabled)
            {
                return false;
            }

            if (context.ActionDescriptor.IsDynamicApmAction())
            {
                return false;
            }

            return AuditingHelper.ShouldSaveAudit(
                context.ActionDescriptor.GetMethodInfoOrNull(),
                _auditingConfiguration,
                ApmSession,
                true
                );
        }

        private string ConvertArgumentsToJson(IDictionary<string, object> arguments)
        {
            try
            {
                if (arguments.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();

                foreach (var argument in arguments)
                {
                    if (argument.Value != null && IgnoredTypesForSerializationOnAuditLogging.Any(t => t.IsInstanceOfType(argument.Value)))
                    {
                        dictionary[argument.Key] = null;
                    }
                    else
                    {
                        dictionary[argument.Key] = argument.Value;
                    }
                }

                return AuditingHelper.Serialize(dictionary);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }
    }
}