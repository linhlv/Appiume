using System;

namespace Appiume.Apm.Tenancy.MultiTenancy
{
    [Serializable]
    public class TenantCacheItem
    {
        public const string CacheName = "ApmTenancyTenantCache";

        public const string ByNameCacheName = "ApmTenancyTenantByNameCache";

        public int Id { get; set; }

        public string Name { get; set; }

        public string TenancyName { get; set; }

        public string ConnectionString { get; set; }

        public int? EditionId { get; set; }

        public bool IsActive { get; set; }

        public object CustomData { get; set; }
    }
}