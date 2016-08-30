using Appiume.Apm.Dependency;
using Appiume.Apm.Runtime.Caching.Configuration;
using Appiume.Apm.Runtime.Caching;

namespace Appiume.Apm.RedisCache
{
    /// <summary>
    /// Extension methods for <see cref="ICachingConfiguration"/>.
    /// </summary>
    public static class RedisCacheConfigurationExtensions
    {
        /// <summary>
        /// Configures caching to use Redis as cache server.
        /// </summary>
        /// <param name="cachingConfiguration">The caching configuration.</param>
        public static void UseRedis(this ICachingConfiguration cachingConfiguration)
        {
            cachingConfiguration.ApmConfiguration.IocManager.RegisterIfNot<ICacheManager, ApmRedisCacheManager>();
        }
    }
}
