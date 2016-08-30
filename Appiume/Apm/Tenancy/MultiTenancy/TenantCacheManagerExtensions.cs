using Appiume.Apm.Runtime.Caching;
using Appiume.Apm.Tenancy.Runtime.Caching;

namespace Appiume.Apm.Tenancy.MultiTenancy
{
    public static class TenantCacheManagerExtensions
    {
        public static ITypedCache<int, TenantCacheItem> GetTenantCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<int, TenantCacheItem>(TenantCacheItem.CacheName);
        }

        public static ITypedCache<string, int?> GetTenantByNameCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<string, int?>(TenantCacheItem.ByNameCacheName);
        }
    }
}