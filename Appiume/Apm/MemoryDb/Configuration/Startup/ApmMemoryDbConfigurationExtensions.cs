using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.MemoryDb.Configuration;

namespace Appiume.Apm.MemoryDb.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure APM MemoryDb module.
    /// </summary>
    public static class ApmMemoryDbConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Apm MemoryDb module.
        /// </summary>
        public static IApmMemoryDbModuleConfiguration ApmMemoryDb(this IModuleConfigurations configurations)
        {
            return configurations.ApmConfiguration.Get<IApmMemoryDbModuleConfiguration>();
        }
    }
}