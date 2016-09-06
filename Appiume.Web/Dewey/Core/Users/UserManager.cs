using Appiume.Apm.Authorization;
using Appiume.Apm.Configuration;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Runtime.Caching;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Apm.Tenancy.Configuration;
using Appiume.Apm.Tenancy.Organizations;
using Appiume.Web.Dewey.Core.Authorization.Roles;
using Appiume.Web.Dewey.Core.MultiTenancy;

namespace Appiume.Web.Dewey.Core.Users
{
    public class UserManager : ApmUserManager<Tenant, Role, User>
    {
        public UserManager(
            UserStore store,
            RoleManager roleManager,
            IRepository<Tenant> tenantRepository,
            IMultiTenancyConfig multiTenancyConfig,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IUserManagementConfig userManagementConfig,
            IIocResolver iocResolver,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository)
            : base(
                store,
                roleManager,
                tenantRepository,
                multiTenancyConfig,
                permissionManager,
                unitOfWorkManager,
                settingManager,
                userManagementConfig,
                iocResolver,
                cacheManager,
                organizationUnitRepository,
                userOrganizationUnitRepository,
                organizationUnitSettings,
                userLoginAttemptRepository
            )
        {
        }
    }
}