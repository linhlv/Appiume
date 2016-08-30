using Appiume.Apm.Authorization;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Runtime.Caching;
using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.Configuration;
using Appiume.Web.IoT.Core.Users;


namespace Appiume.Web.IoT.Core.Authorization.Roles
{
    public class RoleManager : ApmRoleManager<Role, User>
    {
        public RoleManager(
            RoleStore store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                store,
                permissionManager,
                roleManagementConfig,
                cacheManager,
                unitOfWorkManager)
        {
        }
    }
}