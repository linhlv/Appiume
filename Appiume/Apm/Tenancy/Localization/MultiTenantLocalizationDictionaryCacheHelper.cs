using System.Collections.Generic;
using Appiume.Apm.Runtime.Caching;
using Appiume.Apm.Tenancy.Runtime.Caching;

namespace Appiume.Apm.Tenancy.Localization
{
    /// <summary>
    /// A helper to implement localization cache.
    /// </summary>
    public static class MultiTenantLocalizationDictionaryCacheHelper
    {
        /// <summary>
        /// The cache name.
        /// </summary>
        public const string CacheName = "ApmTenancyMultiTenantLocalizationDictionaryCache";

        public static ITypedCache<string, Dictionary<string, string>> GetMultiTenantLocalizationDictionaryCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache(CacheName).AsTyped<string, Dictionary<string, string>>();
        }

        public static string CalculateCacheKey(int? tenantId, string sourceName, string languageName)
        {
            return sourceName + "#" + languageName + "#" + (tenantId ?? 0);
        }
    }
}