using Appiume.Apm.Tenancy.Authorization;
using Appiume.Web.Modules.EventCloud.Core.Authorization.Roles;
using Appiume.Web.Modules.EventCloud.Core.Users;
using Appiume.Web.Modules.EventCloud.Core.MultiTenancy;

namespace Appiume.Web.Modules.EventCloud.Core.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
