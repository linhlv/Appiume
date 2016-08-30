using System.Reflection;
using Appiume.Apm.Modules;
using Appiume.Apm.MongoDb.Configuration;

namespace Appiume.Apm.MongoDb
{
    /// <summary>
    /// This module is used to implement "Data Access Layer" in MongoDB.
    /// </summary>
    [DependsOn(typeof(ApmKernelModule))]
    public class ApmMongoDbModule : ApmModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IApmMongoDbModuleConfiguration, ApmMongoDbModuleConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
