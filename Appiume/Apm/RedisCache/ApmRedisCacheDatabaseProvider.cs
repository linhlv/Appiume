using System;
using System.Configuration;
using Appiume.Apm.Dependency;
using Appiume.Apm.Extensions;
using StackExchange.Redis;

namespace Appiume.Apm.RedisCache
{
    /// <summary>
    /// Implements <see cref="IApmRedisCacheDatabaseProvider"/>.
    /// </summary>
    public class ApmRedisCacheDatabaseProvider : IApmRedisCacheDatabaseProvider, ISingletonDependency
    {
        private const string ConnectionStringKey = "Apm.Redis.Cache";
        private const string DatabaseIdSettingKey = "Apm.Redis.Cache.DatabaseId";

        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApmRedisCacheDatabaseProvider"/> class.
        /// </summary>
        public ApmRedisCacheDatabaseProvider()
        {
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(GetDatabaseId());
        }

        private static ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(GetConnectionString());
        }

        private static int GetDatabaseId()
        {
            var appSetting = ConfigurationManager.AppSettings[DatabaseIdSettingKey];
            if (appSetting.IsNullOrEmpty())
            {
                return -1;
            }

            int databaseId;
            if (!int.TryParse(appSetting, out databaseId))
            {
                return -1;
            }

            return databaseId;
        }

        private static string GetConnectionString()
        {
            var connStr = ConfigurationManager.ConnectionStrings[ConnectionStringKey];
            if (connStr == null || connStr.ConnectionString.IsNullOrWhiteSpace())
            {
                return "localhost";
            }

            return connStr.ConnectionString;
        }
    }
}
