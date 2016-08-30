using System;
using System.Collections.Generic;

namespace Appiume.Apm.Tenancy.Application.Editions
{
    [Serializable]
    public class EditionfeatureCacheItem
    {
        public const string CacheStoreName = "ApmTenancyEditionFeatures";

        public IDictionary<string, string> FeatureValues { get; set; }

        public EditionfeatureCacheItem()
        {
            FeatureValues = new Dictionary<string, string>();
        }
    }
}