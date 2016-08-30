using System.Data.Entity;
using Appiume.Apm.Application.Features;
using Appiume.Apm.Auditing;
using Appiume.Apm.Authorization;
using Appiume.Apm.BackgroundJobs;
using Appiume.Apm.Configuration;
using Appiume.Apm.Localization;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Notifications;
using Appiume.Apm.Tenancy.Application.Editions;
using Appiume.Apm.Tenancy.Application.Features;
using Appiume.Apm.Tenancy.Auditing;
using Appiume.Apm.Tenancy.Authorization;
using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Apm.Tenancy.Configuration;
using Appiume.Apm.Tenancy.Localization;
using Appiume.Apm.Tenancy.MultiTenancy;
using Appiume.Apm.Tenancy.Organizations;

namespace Appiume.Apm.Tenancy.Ef
{
    /// <summary>
    /// Extension methods for <see cref="DbModelBuilder"/>.
    /// </summary>
    public static class ApmTenancyDbModelBuilderExtensions
    {
        /// <summary>
        /// Changes prefix for Apm tables (which is "Apm" by default).
        /// Can be null/empty string to clear the prefix.
        /// </summary>
        /// <typeparam name="TTenant">The type of the tenant entity.</typeparam>
        /// <typeparam name="TRole">The type of the role entity.</typeparam>
        /// <typeparam name="TUser">The type of the user entity.</typeparam>
        /// <param name="modelBuilder">Model builder.</param>
        /// <param name="prefix">Table prefix, or null to clear prefix.</param>
        public static void ChangeApmTablePrefix<TTenant, TRole, TUser>(this DbModelBuilder modelBuilder, string prefix, string schemaName = null)
            where TTenant : ApmTenant<TUser>
            where TRole : ApmRole<TUser>
            where TUser : ApmUser<TUser>
        {
            prefix = prefix ?? "";

            SetTableName<AuditLog>(modelBuilder, prefix + "AuditLogs", schemaName);
            SetTableName<BackgroundJobInfo>(modelBuilder, prefix + "BackgroundJobs", schemaName);
            SetTableName<Edition>(modelBuilder, prefix + "Editions", schemaName);
            SetTableName<FeatureSetting>(modelBuilder, prefix + "Features", schemaName);
            SetTableName<TenantFeatureSetting>(modelBuilder, prefix + "Features", schemaName);
            SetTableName<EditionFeatureSetting>(modelBuilder, prefix + "Features", schemaName);
            SetTableName<ApplicationLanguage>(modelBuilder, prefix + "Languages", schemaName);
            SetTableName<ApplicationLanguageText>(modelBuilder, prefix + "LanguageTexts", schemaName);
            SetTableName<NotificationInfo>(modelBuilder, prefix + "Notifications", schemaName);
            SetTableName<NotificationSubscriptionInfo>(modelBuilder, prefix + "NotificationSubscriptions", schemaName);
            SetTableName<OrganizationUnit>(modelBuilder, prefix + "OrganizationUnits", schemaName);
            SetTableName<PermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            SetTableName<RolePermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            SetTableName<UserPermissionSetting>(modelBuilder, prefix + "Permissions", schemaName);
            SetTableName<TRole>(modelBuilder, prefix + "Roles", schemaName);
            SetTableName<Setting>(modelBuilder, prefix + "Settings", schemaName);
            SetTableName<TTenant>(modelBuilder, prefix + "Tenants", schemaName);
            SetTableName<UserLogin>(modelBuilder, prefix + "UserLogins", schemaName);
            SetTableName<UserLoginAttempt>(modelBuilder, prefix + "UserLoginAttempts", schemaName);
            SetTableName<TenantNotificationInfo>(modelBuilder, prefix + "TenantNotifications", schemaName);
            SetTableName<UserNotificationInfo>(modelBuilder, prefix + "UserNotifications", schemaName);
            SetTableName<UserOrganizationUnit>(modelBuilder, prefix + "UserOrganizationUnits", schemaName);
            SetTableName<UserRole>(modelBuilder, prefix + "UserRoles", schemaName);
            SetTableName<TUser>(modelBuilder, prefix + "Users", schemaName);
            SetTableName<UserAccount>(modelBuilder, prefix + "UserAccounts", schemaName);
        }

        private static void SetTableName<TEntity>(DbModelBuilder modelBuilder, string tableName, string schemaName)
            where TEntity : class
        {
            if (schemaName == null)
            {
                modelBuilder.Entity<TEntity>().ToTable(tableName);
            }
            else
            {
                modelBuilder.Entity<TEntity>().ToTable(tableName, schemaName);                
            }
        }
    }
}