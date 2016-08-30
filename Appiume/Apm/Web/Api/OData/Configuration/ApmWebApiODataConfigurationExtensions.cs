using Appiume.Apm.Configuration.Startup;

namespace Appiume.Apm.Web.WebApi.OData.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Appiume.Apm.Web.WebApi.OData module.
    /// </summary>
    public static class ApmWebApiODataConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Appiume.Apm.Web.WebApi.OData module.
        /// </summary>
        public static IApmWebApiODataModuleConfiguration ApmWebApiOData(this IModuleConfigurations configurations)
        {
            return configurations.ApmConfiguration.Get<IApmWebApiODataModuleConfiguration>();
        }
    }
}