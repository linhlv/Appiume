using System.Reflection;
using Appiume.Apm.AutoMapper;
using Appiume.Apm.Modules;
using Appiume.Web.IoT.Core;

namespace Appiume.Web.IoT.Application
{
    [DependsOn(typeof(IoTCoreModule), typeof(ApmAutoMapperModule))]
    public class IoTApplicationModule : ApmModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
