using System.Collections.Generic;
using System.Linq;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Ef.GraphDiff.Mapping;

namespace Appiume.Apm.Ef.GraphDiff.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Apm.EntityFramework.GraphDiff module.
    /// </summary>
    public static class ApmEntityFrameworkGraphDiffConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Apm.EntityFramework.GraphDiff module.
        /// </summary>
        public static IApmEntityFrameworkGraphDiffModuleConfiguration ApmEfGraphDiff(this IModuleConfigurations configurations)
        {
            return configurations.ApmConfiguration.Get<IApmEntityFrameworkGraphDiffModuleConfiguration>();
        }

        /// <summary>
        /// Used to provide a mappings for the Apm.EntityFramework.GraphDiff module.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="entityMappings"></param>
        public static void UseMappings(this IApmEntityFrameworkGraphDiffModuleConfiguration configuration, IEnumerable<EntityMapping> entityMappings)
        {
            configuration.EntityMappings = entityMappings.ToList();
        }
    }
}
