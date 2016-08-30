using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using Appiume.Apm.Auditing;

namespace Appiume.Apm.Web.Mvc.Auditing
{
    public class ApmAuditFilterData
    {
        private const string ApmAuditFilterDataHttpContextKey = "__ApmAuditFilterData";

        public Stopwatch Stopwatch { get; }

        public AuditInfo AuditInfo { get; }

        public ApmAuditFilterData(
            Stopwatch stopwatch,
            AuditInfo auditInfo)
        {
            Stopwatch = stopwatch;
            AuditInfo = auditInfo;
        }

        public static void Set(HttpContextBase httpContext, ApmAuditFilterData auditFilterData)
        {
            GetAuditDataStack(httpContext).Push(auditFilterData);
        }

        public static ApmAuditFilterData GetOrNull(HttpContextBase httpContext)
        {
            var stack = GetAuditDataStack(httpContext);
            return stack.Count <= 0
                ? null
                : stack.Pop();
        }

        private static Stack<ApmAuditFilterData> GetAuditDataStack(HttpContextBase httpContext)
        {
            var stack = httpContext.Items[ApmAuditFilterDataHttpContextKey] as Stack<ApmAuditFilterData>;

            if (stack == null)
            {
                stack = new Stack<ApmAuditFilterData>();
                httpContext.Items[ApmAuditFilterDataHttpContextKey] = stack;
            }

            return stack;
        }
    }
}