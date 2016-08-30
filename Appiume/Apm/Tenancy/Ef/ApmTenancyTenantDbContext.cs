using System.Data.Common;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.Authorization.Users;

namespace Appiume.Apm.Tenancy.Ef
{
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class ApmTenancyTenantDbContext<TRole, TUser> : ApmTenancyCommonDbContext<TRole, TUser>
        where TRole : ApmRole<TUser>
        where TUser : ApmUser<TUser>
    {
        /// <summary>
        /// Default constructor.
        /// Do not directly instantiate this class. Instead, use dependency injection!
        /// </summary>
        protected ApmTenancyTenantDbContext()
        {

        }

        /// <summary>
        /// Constructor with connection string parameter.
        /// </summary>
        /// <param name="nameOrConnectionString">Connection string or a name in connection strings in configuration file</param>
        protected ApmTenancyTenantDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// This constructor can be used for unit tests.
        /// </summary>
        protected ApmTenancyTenantDbContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {

        }
    }
}