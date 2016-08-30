using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Apm.Threading;

namespace Appiume.Apm.Tenancy.MultiTenancy
{
    //TODO: Create other sync extension methods.
    public static class ApmTenantManagerExtensions
    {
        public static TTenant GetById<TTenant, TRole, TUser>(this ApmTenantManager<TTenant, TRole, TUser> tenantManager, int id)
            where TTenant : ApmTenant<TUser>
            where TRole : ApmRole<TUser>
            where TUser : ApmUser<TUser>
        {
            return AsyncHelper.RunSync(() => tenantManager.GetByIdAsync(id));
        }
    }
}