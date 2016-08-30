namespace Appiume.Apm.Tenancy.MultiTenancy
{
    public interface ITenantCache
    {
        TenantCacheItem Get(int tenantId);

        TenantCacheItem Get(string tenancyName);

        TenantCacheItem GetOrNull(string tenancyName);

        TenantCacheItem GetOrNull(int tenantId);
    }
}