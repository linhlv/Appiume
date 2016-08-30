using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Configuration.Startup;

namespace Appiume.Apm.Web.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Apm Web module.
    /// </summary>
    public static class ApmWebConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Apm Web module.
        /// </summary>
        public static IApmWebModuleConfiguration ApmWeb(this IModuleConfigurations configurations)
        {
            return configurations.ApmConfiguration.Get<IApmWebModuleConfiguration>();
        }
    }
}