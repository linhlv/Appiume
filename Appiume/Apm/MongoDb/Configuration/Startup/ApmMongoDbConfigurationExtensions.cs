using Appiume.Apm.MongoDb.Configuration;
using Appiume.Apm.Configuration.Startup;

namespace Appiume.Apm.MongoDb.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Apm MongoDb module.
    /// </summary>
    public static class ApmMongoDbConfigurationExtensions
    {
        /// <summary>
        /// Used to configure APM MongoDb module.
        /// </summary>
        public static IApmMongoDbModuleConfiguration ApmMongoDb(this IModuleConfigurations configurations)
        {
            return configurations.ApmConfiguration.Get<IApmMongoDbModuleConfiguration>();
        }
    }
}