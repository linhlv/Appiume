using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Modules.EventCloud.Core.Users;

namespace Appiume.Web.Modules.EventCloud.Core.Authorization.Roles
{
    public class RoleStore : ApmRoleStore<Role, User>
    {
        public RoleStore(
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(
                roleRepository,
                userRoleRepository,
                rolePermissionSettingRepository)
        {
        }
    }
}