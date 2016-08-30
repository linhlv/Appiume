using System.Threading.Tasks;
using Appiume.Apm.Dependency;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Runtime.Session;
using Castle.Core.Logging;
using Appiume.Apm.Authorization;
using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Apm.Tenancy.MultiTenancy;

namespace Appiume.Apm.Tenancy.Authorization
{
    /// <summary>
    /// Application should inherit this class to implement <see cref="IPermissionChecker"/>.
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public abstract class PermissionChecker<TTenant, TRole, TUser> : IPermissionChecker, ITransientDependency
        where TRole : ApmRole<TUser>, new()
        where TUser : ApmUser<TUser>
        where TTenant : ApmTenant<TUser>
    {
        private readonly ApmUserManager<TTenant, TRole, TUser> _userManager;

        public ILogger Logger { get; set; }

        public IApmSession ApmSession { get; set; }

        public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected PermissionChecker(ApmUserManager<TTenant, TRole, TUser> userManager)
        {
            _userManager = userManager;

            Logger = NullLogger.Instance;
            ApmSession = NullApmSession.Instance;
        }

        public virtual async Task<bool> IsGrantedAsync(string permissionName)
        {
            return ApmSession.UserId.HasValue && await _userManager.IsGrantedAsync(ApmSession.UserId.Value, permissionName);
        }

        public virtual async Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            return await _userManager.IsGrantedAsync(userId, permissionName);
        }

        [UnitOfWork]
        public virtual async Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            if (CurrentUnitOfWorkProvider == null || CurrentUnitOfWorkProvider.Current == null)
            {
                return await IsGrantedAsync(user.UserId, permissionName);
            }

            using (CurrentUnitOfWorkProvider.Current.SetTenantId(user.TenantId))
            {
                return await _userManager.IsGrantedAsync(user.UserId, permissionName);
            }
        }
    }
}
