using System.Data.Common;
using System.Data.Entity;
using Appiume.Apm.Tenancy.Application.Editions;
using Appiume.Apm.Application.Features;
using Appiume.Apm.BackgroundJobs;
using Appiume.Apm.Notifications;
using Appiume.Apm.Tenancy.Application.Features;
using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Apm.Tenancy.Ef.Extensions;
using Appiume.Apm.Tenancy.Ef;
using Appiume.Apm.Tenancy.MultiTenancy;

namespace Appiume.Apm.Tenancy.Ef
{
    /// <summary>
    /// Base DbContext for Apm zero.
    /// Derive your DbContext from this class to have base entities.
    /// </summary>
    public abstract class ApmTenancyDbContext<TTenant, TRole, TUser> : ApmTenancyCommonDbContext<TRole, TUser>
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
        protected ApmTenancyDbContext()
        {

        }

        /// <summary>
        /// Constructor with connection string parameter.
        /// </summary>
        /// <param name="nameOrConnectionString">Connection string or a name in connection strings in configuration file</param>
        protected ApmTenancyDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// This constructor can be used for unit tests.
        /// </summary>
        protected ApmTenancyDbContext(DbConnection dbConnection, bool contextOwnsConnection)
            : base(dbConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region BackgroundJobInfo.IX_IsAbandoned_NextTryTime

            modelBuilder.Entity<BackgroundJobInfo>()
                .Property(j => j.IsAbandoned)
                .CreateIndex("IX_IsAbandoned_NextTryTime", 1);

            modelBuilder.Entity<BackgroundJobInfo>()
                .Property(j => j.NextTryTime)
                .CreateIndex("IX_IsAbandoned_NextTryTime", 2);

            #endregion

            #region NotificationSubscriptionInfo.IX_NotificationName_EntityTypeName_EntityId_UserId

            modelBuilder.Entity<NotificationSubscriptionInfo>()
                .Property(ns => ns.NotificationName)
                .CreateIndex("IX_NotificationName_EntityTypeName_EntityId_UserId", 1);

            modelBuilder.Entity<NotificationSubscriptionInfo>()
                .Property(ns => ns.EntityTypeName)
                .CreateIndex("IX_NotificationName_EntityTypeName_EntityId_UserId", 2);

            modelBuilder.Entity<NotificationSubscriptionInfo>()
                .Property(ns => ns.EntityId)
                .CreateIndex("IX_NotificationName_EntityTypeName_EntityId_UserId", 3);

            modelBuilder.Entity<NotificationSubscriptionInfo>()
                .Property(ns => ns.UserId)
                .CreateIndex("IX_NotificationName_EntityTypeName_EntityId_UserId", 4);

            #endregion

            #region UserNotificationInfo.IX_UserId_State_CreationTime

            modelBuilder.Entity<UserNotificationInfo>()
                .Property(un => un.UserId)
                .CreateIndex("IX_UserId_State_CreationTime", 1);

            modelBuilder.Entity<UserNotificationInfo>()
                .Property(un => un.State)
                .CreateIndex("IX_UserId_State_CreationTime", 2);

            modelBuilder.Entity<UserNotificationInfo>()
                .Property(un => un.CreationTime)
                .CreateIndex("IX_UserId_State_CreationTime", 3);

            #endregion

            #region UserLoginAttempt.IX_TenancyName_UserNameOrEmailAddress_Result

            modelBuilder.Entity<UserLoginAttempt>()
                .Property(ula => ula.TenancyName)
                .CreateIndex("IX_TenancyName_UserNameOrEmailAddress_Result", 1);

            modelBuilder.Entity<UserLoginAttempt>()
                .Property(ula => ula.UserNameOrEmailAddress)
                .CreateIndex("IX_TenancyName_UserNameOrEmailAddress_Result", 2);

            modelBuilder.Entity<UserLoginAttempt>()
                .Property(ula => ula.Result)
                .CreateIndex("IX_TenancyName_UserNameOrEmailAddress_Result", 3);

            #endregion

            #region UserLoginAttempt.IX_UserId_TenantId

            modelBuilder.Entity<UserLoginAttempt>()
                .Property(ula => ula.UserId)
                .CreateIndex("IX_UserId_TenantId", 1);

            modelBuilder.Entity<UserLoginAttempt>()
                .Property(ula => ula.TenantId)
                .CreateIndex("IX_UserId_TenantId", 2);

            #endregion
        }
    }
}
