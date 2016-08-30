namespace Appiume.Apm.Tenancy.MultiTenancy
{
    public interface IApmTenancyDbMigrator
    {
        void CreateOrMigrateForHost();

        void CreateOrMigrateForTenant(ApmTenantBase tenant);
    }
}
