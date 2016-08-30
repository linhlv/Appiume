using Appiume.Apm.Dependency;
using Appiume.Apm.RedisCache;
using Appiume.Apm.Runtime.Caching;
using Appiume.Apm.Runtime.Caching.Configuration;

namespace Appiume.Apm.RedisCache
{
    /// <summary>
    /// Used to create <see cref="ApmRedisCache"/> instances.
    /// </summary>
    public class ApmRedisCacheManager : CacheManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApmRedisCacheManager"/> class.
        /// </summary>
        public ApmRedisCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            IocManager.RegisterIfNot<ApmRedisCache>(DependencyLifeStyle.Transient);
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.Resolve<ApmRedisCache>(new { name });
        }
    }
}
