using Appiume.Apm.Configuration.Startup;

namespace Appiume.Apm.Web.Mvc.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Appiume.Apm.Web.WebApi module.
    /// </summary>
    public static class ApmMvcConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Appiume.Apm.Web.WebApi module.
        /// </summary>
        public static IApmMvcConfiguration ApmMvc(this IModuleConfigurations configurations)
        {
            return configurations.ApmConfiguration.Get<IApmMvcConfiguration>();
        }
    }
}