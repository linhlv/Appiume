using System.Collections.Generic;
using Appiume.Apm.Ef.GraphDiff.Mapping;

namespace Appiume.Apm.Ef.GraphDiff.Configuration
{
    public class ApmEntityFrameworkGraphDiffModuleConfiguration : IApmEntityFrameworkGraphDiffModuleConfiguration
    {
        public List<EntityMapping> EntityMappings { get; set; }
    }
}