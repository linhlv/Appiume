using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Appiume.Apm.Dependency;
using Appiume.Apm.Localization;
using Appiume.Apm.Modules;
using Appiume.Apm.Web.Mvc;
using Appiume.Web.IoT.Api;
using Appiume.Web.IoT.Application;
using Appiume.Web.IoT.Ef;
using Appiume.Web.IoT.Navigation;
using Appiume.Web.IoT.Web;

namespace Appiume.Web
{
    [DependsOn(
        typeof(IoTDataModule),
        typeof(IoTApplicationModule),
        typeof(IoTWebApiModule),
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
            Configuration.Navigation.Providers.Add<IoTNavigationProvider>();
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
