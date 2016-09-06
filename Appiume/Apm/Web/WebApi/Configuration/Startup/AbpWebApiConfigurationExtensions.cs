using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Web.WebApi.Configuration;

namespace Appiume.Apm.Web.WebApi.Configuration.Startup
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Abp.Web.Api module.
    /// </summary>
    public static class ApmWebApiConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Abp.Web.Api module.
        /// </summary>
        public static IApmWebApiConfiguration ApmWebApi(this IModuleConfigurations configurations)
        {
            return configurations.ApmConfiguration.Get<IApmWebApiConfiguration>();
        }
    }
}