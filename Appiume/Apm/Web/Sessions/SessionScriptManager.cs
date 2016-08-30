using System.Text;
using Appiume.Apm.Dependency;
using Appiume.Apm.Runtime.Session;

namespace Appiume.Apm.Web.Sessions
{
    public class SessionScriptManager : ISessionScriptManager, ITransientDependency
    {
        public IApmSession ApmSession { get; set; }

        public SessionScriptManager()
        {
            ApmSession = NullApmSession.Instance;
        }

        public string GetScript()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine();

            script.AppendLine("    apm.session = apm.session || {};");
            script.AppendLine("    apm.session.userId = " + (ApmSession.UserId.HasValue ? ApmSession.UserId.Value.ToString() : "null") + ";");
            script.AppendLine("    apm.session.tenantId = " + (ApmSession.TenantId.HasValue ? ApmSession.TenantId.Value.ToString() : "null") + ";");
            script.AppendLine("    apm.session.impersonatorUserId = " + (ApmSession.ImpersonatorUserId.HasValue ? ApmSession.ImpersonatorUserId.Value.ToString() : "null") + ";");
            script.AppendLine("    apm.session.impersonatorTenantId = " + (ApmSession.ImpersonatorTenantId.HasValue ? ApmSession.ImpersonatorTenantId.Value.ToString() : "null") + ";");
            script.AppendLine("    apm.session.multiTenancySide = " + ((int)ApmSession.MultiTenancySide) + ";");

            script.AppendLine();
            script.Append("})();");

            return script.ToString();
        }
    }
}