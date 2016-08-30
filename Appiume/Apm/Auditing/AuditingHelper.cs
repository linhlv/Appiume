using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Appiume.Apm.Runtime.Session;
using Newtonsoft.Json;

namespace Appiume.Apm.Auditing
{
    public static class AuditingHelper
    {
        public static bool ShouldSaveAudit(MethodInfo methodInfo, IAuditingConfiguration configuration, IApmSession apmSession, bool defaultValue = false)
        {
            if (configuration == null || !configuration.IsEnabled)
            {
                return false;
            }

            if (!configuration.IsEnabledForAnonymousUsers && (apmSession == null || !apmSession.UserId.HasValue))
            {
                return false;
            }

            if (methodInfo == null)
            {
                return false;
            }

            if (!methodInfo.IsPublic)
            {
                return false;
            }

            if (methodInfo.IsDefined(typeof(AuditedAttribute)))
            {
                return true;
            }

            if (methodInfo.IsDefined(typeof(DisableAuditingAttribute)))
            {
                return false;
            }

            var classType = methodInfo.DeclaringType;
            if (classType != null)
            {
                if (classType.IsDefined(typeof(AuditedAttribute)))
                {
                    return true;
                }

                if (classType.IsDefined(typeof(DisableAuditingAttribute)))
                {
                    return false;
                }

                if (configuration.Selectors.Any(selector => selector.Predicate(classType)))
                {
                    return true;
                }
            }

            return defaultValue;
        }

        public static string Serialize(object obj)
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new AuditingContractResolver()
            };

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}