using System.Collections.Generic;
using System.Reflection;
using Appiume.Apm.Ef.GraphDiff.Configuration;
using Appiume.Apm.Ef.GraphDiff.Mapping;
using Appiume.Apm.Modules;

namespace Appiume.Apm.Ef.GraphDiff
{
    [DependsOn(typeof(ApmEntityFrameworkModule), typeof(ApmKernelModule))]
    public class ApmEntityFrameworkGraphDiffModule : ApmModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IApmEntityFrameworkGraphDiffModuleConfiguration, ApmEntityFrameworkGraphDiffModuleConfiguration>();

            Configuration.Modules
                .ApmEfGraphDiff()
                .UseMappings(new List<EntityMapping>());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
