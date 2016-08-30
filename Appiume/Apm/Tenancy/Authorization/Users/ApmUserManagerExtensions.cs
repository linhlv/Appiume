using System;
using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.MultiTenancy;
using Appiume.Apm.Threading;

namespace Appiume.Apm.Tenancy.Authorization.Users
{
    /// <summary>
    /// Extension methods for <see cref="ApmUserManager{TTenant,TRole,TUser}"/>.
    /// </summary>
    public static class ApmUserManagerExtensions
    {
        /// <summary>
        /// Check whether a user is granted for a permission.
        /// </summary>
        /// <param name="manager">User manager</param>
        /// <param name="userId">User id</param>
        /// <param name="permissionName">Permission name</param>
        public static bool IsGranted<TTenant, TRole, TUser>(ApmUserManager<TTenant, TRole, TUser> manager, long userId, string permissionName)
            where TTenant : ApmTenant<TUser>
            where TRole : ApmRole<TUser>, new()
            where TUser : ApmUser<TUser>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }

            return AsyncHelper.RunSync(() => manager.IsGrantedAsync(userId, permissionName));
        }

        public static ApmUserManager<TTenant, TRole, TUser>.ApmLoginResult Login<TTenant, TRole, TUser>(ApmUserManager<TTenant, TRole, TUser> manager, string userNameOrEmailAddress, string plainPassword, string tenancyName = null)
            where TTenant : ApmTenant<TUser>
            where TRole : ApmRole<TUser>, new()
            where TUser : ApmUser<TUser>
        {
            if (manager == null)
            {
                throw new ArgumentNullException("manager");
            }

            return AsyncHelper.RunSync(() => manager.LoginAsync(userNameOrEmailAddress, plainPassword, tenancyName));
        }
    }
}