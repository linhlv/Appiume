using System.Reflection;
using Appiume.Apm.MemoryDb.Configuration;
using Appiume.Apm.Modules;

namespace Appiume.Apm.MemoryDb
{
    /// <summary>
    /// This module is used to implement "Data Access Layer" in MemoryDb.
    /// </summary>
    public class ApmMemoryDbModule : ApmModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IApmMemoryDbModuleConfiguration, ApmMemoryDbModuleConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
