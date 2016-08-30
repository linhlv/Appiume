using System.Data.Common;
using System.Data.Entity;
using Appiume.Apm.BackgroundJobs;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Tenancy.Application.Editions;
using Appiume.Apm.Tenancy.Application.Features;
using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Apm.Tenancy.MultiTenancy;
using Appiume.Apm.Tenancy.Application.Editions;
using Appiume.Apm.Application.Features;
using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Apm.BackgroundJobs;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Tenancy.Application.Features;
using Appiume.Apm.Tenancy.MultiTenancy;

namespace Appiume.Apm.Tenancy.Ef
{
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class ApmTenancyHostDbContext<TTenant, TRole, TUser> : ApmTenancyCommonDbContext<TRole, TUser>
        where TTenant : ApmTenant<TUser>
        where TRole : ApmRole<TUser>
        where TUser : ApmUser<TUser>
    {
        /// <summary>
        /// Tenants
        /// </summary>
        public virtual IDbSet<TTenant> Tenants { get; set; }

        /// <summary>
        /// Editions.
        /// </summary>
        public virtual IDbSet<Edition> Editions { get; set; }

        /// <summary>
        /// FeatureSettings.
        /// </summary>
        public virtual IDbSet<FeatureSetting> FeatureSettings { get; set; }

        /// <summary>
        /// TenantFeatureSetting.
        /// </summary>
        public virtual IDbSet<TenantFeatureSetting> TenantFeatureSettings { get; set; }

        /// <summary>
        /// EditionFeatureSettings.
        /// </summary>
        public virtual IDbSet<EditionFeatureSetting> EditionFeatureSettings { get; set; }

        /// <summary>
        /// Background jobs.
        /// </summary>
        public virtual IDbSet<BackgroundJobInfo> BackgroundJobs { get; set; }

        /// <summary>
        /// User accounts
        /// </summary>
        public virtual IDbSet<UserAccount> UserAccounts { get; set; }

        /// <summary>
        /// Default constructor.
        /// Do not directly instantiate this class. Instead, use dependency injection!
        /// </summary>
        protected ApmTenancyHostDbContext()
        {

        }

        /// <summary>
        /// Constructor with connection string parameter.
        /// </summary>
        /// <param name="nameOrConnectionString">Connection string or a name in connection strings in configuration file</param>
        protected ApmTenancyHostDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// This constructor can be used for unit tests.
        /// </summary>
        protected ApmTenancyHostDbContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {

        }
    }
}