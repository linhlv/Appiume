using StackExchange.Redis;

namespace Appiume.Apm.RedisCache
{
    /// <summary>
    /// Used to get <see cref="IDatabase"/> for Redis cache.
    /// </summary>
    public interface IApmRedisCacheDatabaseProvider 
    {
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        IDatabase GetDatabase();
    }
}
