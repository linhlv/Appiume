using System.Reflection;
using Appiume.Apm.AutoMapper;
using Appiume.Apm.Modules;
using Appiume.Web.Modules.EventCloud.Core;

namespace Appiume.Web.Modules.EventCloud.Application
{
    [DependsOn(typeof(EventCloudCoreModule), typeof(ApmAutoMapperModule))]
    public class EventCloudApplicationModule : ApmModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
