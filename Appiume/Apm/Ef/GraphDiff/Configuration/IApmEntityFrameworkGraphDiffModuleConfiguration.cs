using System.Collections.Generic;
using Appiume.Apm.Ef.GraphDiff.Mapping;

namespace Appiume.Apm.Ef.GraphDiff.Configuration
{
    public interface IApmEntityFrameworkGraphDiffModuleConfiguration
    {
        List<EntityMapping> EntityMappings { get; set; }
    }
}