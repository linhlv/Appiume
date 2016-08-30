using System.Reflection;
using Appiume.Apm.Modules;

namespace Appiume.Apm.RedisCache
{
    /// <summary>
    /// This modules is used to replace Apm's cache system with Redis server.
    /// </summary>
    [DependsOn(typeof(ApmKernelModule))]
    public class ApmRedisCacheModule : ApmModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
