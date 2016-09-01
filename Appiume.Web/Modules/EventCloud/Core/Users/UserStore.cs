using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Modules.EventCloud.Core.Authorization.Roles;

namespace Appiume.Web.Modules.EventCloud.Core.Users
{
    public class UserStore : ApmUserStore<Role, User>
    {
        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
              userRepository,
              userLoginRepository,
              userRoleRepository,
              roleRepository,
              userPermissionSettingRepository,
              unitOfWorkManager)
        {
        }
    }
}