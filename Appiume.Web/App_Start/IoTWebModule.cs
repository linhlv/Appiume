using System.Reflection;
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

namespace Appiume.Web
{
    [DependsOn(
        typeof(EventCloudDataModule),
        typeof(EventCloudApplicationModule),
        typeof(EventCloudWebApiModule),
        typeof(ApmWebMvcModule)
        )]
    public class IoTWebModule : ApmModule
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

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
