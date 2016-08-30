using Appiume.Apm.Configuration.Startup;

namespace Appiume.Apm.Tenancy.Configuration
{
    /// <summary>
    /// Extension methods for module zero configurations.
    /// </summary>
    public static class ModuleTenancyConfigurationExtensions
    {
        /// <summary>
        /// Used to configure module zero.
        /// </summary>
        /// <param name="moduleConfigurations"></param>
        /// <returns></returns>
        public static IApmTenancyConfig Tenancy(this IModuleConfigurations moduleConfigurations)
        {
            return moduleConfigurations.ApmConfiguration
                .GetOrCreate("ApmTenancyConfig",
                    () => moduleConfigurations.ApmConfiguration.IocManager.Resolve<IApmTenancyConfig>()
                );
        }
    }
}