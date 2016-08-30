using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appiume.Apm.Auditing;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Dependency;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.Timing;
using Appiume.Apm.Web.Mvc.Extensions;
using Castle.Core.Logging;

namespace Appiume.Apm.Web.Mvc.Auditing
{
    public class ApmMvcAuditFilter : IActionFilter, ITransientDependency
    {
        /// <summary>
        /// Ignored types for serialization on audit logging.
        /// </summary>
        public static List<Type> IgnoredTypesForSerializationOnAuditLogging { get; private set; }

        public IApmSession ApmSession { get; set; }
        public IAuditInfoProvider AuditInfoProvider { get; set; }
        public IAuditingStore AuditingStore { get; set; }
        public ILogger Logger { get; set; }

        private readonly IAuditingConfiguration _auditingConfiguration;

        static ApmMvcAuditFilter()
        {
            IgnoredTypesForSerializationOnAuditLogging = new List<Type>
            {
                typeof (HttpPostedFileBase),
                typeof (IEnumerable<HttpPostedFileBase>)
            };
        }

        public ApmMvcAuditFilter(IAuditingConfiguration auditingConfiguration)
        {
            _auditingConfiguration = auditingConfiguration;

            ApmSession = NullApmSession.Instance;
            AuditingStore = SimpleLogAuditingStore.Instance;
            AuditInfoProvider = NullAuditInfoProvider.Instance;
            Logger = NullLogger.Instance;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!ShouldSaveAudit(filterContext))
            {
                ApmAuditFilterData.Set(filterContext.HttpContext, null);
                return;
            }

            var currentMethodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            var actionStopwatch = Stopwatch.StartNew();
            var auditInfo = new AuditInfo
            {
                TenantId = ApmSession.TenantId,
                UserId = ApmSession.UserId,
                ImpersonatorUserId = ApmSession.ImpersonatorUserId,
                ImpersonatorTenantId = ApmSession.ImpersonatorTenantId,
                ServiceName = currentMethodInfo.DeclaringType != null
                                ? currentMethodInfo.DeclaringType.FullName
                                : filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                MethodName = currentMethodInfo.Name,
                Parameters = ConvertArgumentsToJson(filterContext),
                ExecutionTime = Clock.Now
            };

            ApmAuditFilterData.Set(
                filterContext.HttpContext,
                new ApmAuditFilterData(
                    actionStopwatch,
                    auditInfo
                )
            );
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var auditData = ApmAuditFilterData.GetOrNull(filterContext.HttpContext);
            if (auditData == null)
            {
                return;
            }

            auditData.Stopwatch.Stop();

            auditData.AuditInfo.ExecutionDuration = Convert.ToInt32(auditData.Stopwatch.Elapsed.TotalMilliseconds);
            auditData.AuditInfo.Exception = filterContext.Exception;

            AuditInfoProvider?.Fill(auditData.AuditInfo);
            AuditingStore.Save(auditData.AuditInfo);
        }

        private bool ShouldSaveAudit(ActionExecutingContext filterContext)
        {
            var currentMethodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            if (currentMethodInfo == null)
            {
                return false;
            }

            if (_auditingConfiguration == null)
            {
                return false;
            }

            if (!_auditingConfiguration.MvcControllers.IsEnabled)
            {
                return false;
            }

            if (filterContext.IsChildAction && !_auditingConfiguration.MvcControllers.IsEnabledForChildActions)
            {
                return false;
            }

            return AuditingHelper.ShouldSaveAudit(
                currentMethodInfo,
                _auditingConfiguration,
                ApmSession,
                true
                );
        }

        private string ConvertArgumentsToJson(ActionExecutingContext filterContext)
        {
            try
            {
                if (filterContext.ActionParameters.IsNullOrEmpty())
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();

                foreach (var argument in filterContext.ActionParameters)
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
                Logger.Warn("Could not serialize arguments for method: " + filterContext.Controller.GetType().FullName + "." + filterContext.ActionDescriptor.ActionName);
                Logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }
    }
}
