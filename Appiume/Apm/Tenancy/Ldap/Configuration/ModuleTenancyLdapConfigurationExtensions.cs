using Appiume.Apm.Configuration.Startup;

namespace Appiume.Apm.Tenancy.Ldap.Configuration
{
    /// <summary>
    /// Extension methods for module zero configurations.
    /// </summary>
    public static class ModuleTenancyLdapConfigurationExtensions
    {
        /// <summary>
        /// Configures Apm Tenancy LDAP module.
        /// </summary>
        /// <returns></returns>
        public static IApmTenancyLdapModuleConfig TenanacyLdap(this IModuleConfigurations moduleConfigurations)
        {
            return moduleConfigurations.ApmConfiguration
                .GetOrCreate("ApmTenancyLdapConfig",
                    () => moduleConfigurations.ApmConfiguration.IocManager.Resolve<IApmTenancyLdapModuleConfig>()
                );
        }
    }
}
