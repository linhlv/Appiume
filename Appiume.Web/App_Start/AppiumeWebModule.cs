using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Appiume.Apm.Dependency;
using Appiume.Apm.Localization;
using Appiume.Apm.Modules;
using Appiume.Apm.Web.Mvc;
using Appiume.Web.IoT.Web;
using Appiume.Web.Modules.EventCloud.Application;
using Appiume.Web.Modules.EventCloud.EntityFramework;
using Appiume.Web.Modules.EventCloud.WebApi;
using Appiume.Web.Modules.EventCloud.WebMvc.Navigation;
using Appiume.Web.Modules.TaskCloud.Application;
using Appiume.Web.Modules.TaskCloud.EntityFramework;
using Appiume.Web.Modules.TaskCloud.WebApi;

namespace Appiume.Web
{
    [DependsOn(
        typeof(EventCloudDataModule),
        typeof(EventCloudApplicationModule),
        typeof(EventCloudWebApiModule),

         typeof(TaskCloudDataModule),
        typeof(TaskCloudApplicationModule),
        typeof(TaskCloudWebApiModule),

        typeof(ApmWebMvcModule)
        )]
    public class AppiumeWebModule : ApmModule
    {
        public override void PreInitialize()
        {
            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("tr", "Türkçe", "famfamfam-flag-tr"));

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<EventCloudNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            EnableCors();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static void EnableCors()
        {
            //This method enables cross origin request

            var cors = new EnableCorsAttribute("*", "*", "*");
            GlobalConfiguration.Configuration.EnableCors(cors);

            //Then, we can call getTasks method from any web site like that:

            /*
             
                 $.ajax({
                    url: 'http://localhost:6247/api/services/tasksystem/task/GetTasks',
                    type: "POST",
                    dataType: 'json',
                    contentType: 'application/json',
                    data: JSON.stringify({})
                }).done(function(result) {
                    console.log(result);
                });
             
             */
        }
    }
}
