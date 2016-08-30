using System.Reflection;
using System.Web.Http;
using Appiume.Apm.Application.Services;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Modules;
using Appiume.Apm.Web.WebApi;
using Appiume.Apm.Web.WebApi.Configuration.Startup;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic.Builders;
using Appiume.Web.IoT.Application;

namespace Appiume.Web.IoT.Api
{
    [DependsOn(typeof(ApmWebApiModule), typeof(IoTApplicationModule))]
    public class IoTWebApiModule : ApmModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(IoTApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.ApmWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
