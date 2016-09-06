using Appiume.Apm.Tenancy.Authorization;
using Appiume.Web.Dewey.Core.Authorization.Roles;
using Appiume.Web.Dewey.Core.Users;
using Appiume.Web.Dewey.Core.MultiTenancy;

namespace Appiume.Web.Dewey.Core.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
