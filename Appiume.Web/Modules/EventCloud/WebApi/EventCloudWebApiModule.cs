using System.Reflection;
using System.Web.Http;
using Appiume.Apm.Application.Services;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Modules;
using Appiume.Apm.Web.WebApi;
using Appiume.Apm.Web.WebApi.Configuration.Startup;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic.Builders;
using Appiume.Web.Modules.EventCloud.Application;


namespace Appiume.Web.Modules.EventCloud.WebApi
{
    [DependsOn(typeof(ApmWebApiModule), typeof(EventCloudApplicationModule))]
    public class EventCloudWebApiModule : ApmModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //DynamicApiControllerBuilder
            //    .ForAll<IApplicationService>(typeof(EventCloudApplicationModule).Assembly, "app")
            //    .Build();

            Configuration.Modules.ApmWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
