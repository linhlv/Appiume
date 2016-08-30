using System.Reflection;
using Appiume.Apm.Hangfire.Configuration;
using Appiume.Apm.Modules;
using Hangfire;

namespace Appiume.Apm.Hangfire
{
    [DependsOn(typeof(ApmKernelModule))]
    public class ApmHangfireModule : ApmModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IApmHangfireConfiguration, ApmHangfireConfiguration>();
            
            Configuration.Modules
                .ApmHangfire()
                .GlobalConfiguration
                .UseActivator(new HangfireIocJobActivator(IocManager));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
