using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Appiume.Apm.Application.Services;
using Appiume.Apm.AutoMapper;
using Appiume.Apm.Dependency;
using Appiume.Apm.Ef;
using Appiume.Apm.Localization;
using Appiume.Apm.Localization.Dictionaries;
using Appiume.Apm.Localization.Dictionaries.Xml;
using Appiume.Apm.Modules;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Tenancy;
using Appiume.Apm.Tenancy.Configuration;
using Appiume.Apm.Timing;
using Appiume.Apm.Web.Mvc;
using Appiume.Apm.Web.WebApi;
using Appiume.Apm.Web.WebApi.Configuration.Startup;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic.Builders;
using Appiume.Web.Dewey.Application;
using Appiume.Web.Dewey.Core;
using Appiume.Web.Dewey.Core.Authorization.Roles;
using Appiume.Web.Dewey.Core.Configuration;
using Appiume.Web.Dewey.EntityFramework;
using Appiume.Web.Dewey.WebMvc.Navigation;

namespace Appiume.Web
{
    [DependsOn(
        typeof(ApmWebMvcModule),
        typeof(ApmWebApiModule),
        typeof(ApmEntityFrameworkModule),
        typeof(ApmAutoMapperModule),
        typeof(ApmTenancyCoreModule)
        )]
    public class AppiumeWebModule : ApmModule
    {
        public override void PreInitialize()
        {
            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("tr", "Türkçe", "famfamfam-flag-tr"));

            //Add a localization source
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "TaskCloud",
                    new XmlFileLocalizationDictionaryProvider(
                        HttpContext.Current.Server.MapPath("~/Dewey/Core/Localization")
                        )
                    )
                );

            Configuration.MultiTenancy.IsEnabled = true;

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    EventCloudConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Appiume.Web.Dewey.Core.Localization.Source"
                        )
                    )
                );

            Configuration.DefaultNameOrConnectionString = "Default";

            Configuration.Settings.Providers.Add<EventCloudSettingProvider>();

            Configuration.Modules.Tenancy().RoleManagement.StaticRoles.Add(new StaticRoleDefinition(StaticRoleNames.Tenant.Admin, MultiTenancySides.Host));
            Configuration.Modules.Tenancy().RoleManagement.StaticRoles.Add(new StaticRoleDefinition(StaticRoleNames.Tenant.Admin, MultiTenancySides.Tenant));
            Configuration.Modules.Tenancy().RoleManagement.StaticRoles.Add(new StaticRoleDefinition(StaticRoleNames.Tenant.Member, MultiTenancySides.Tenant));

            Clock.Provider = new UtcClockProvider();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<EventCloudNavigationProvider>();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<TaskCloudNavigationProvider>();

            //We must declare mappings to be able to use AutoMapper
            DtoMappings.Map();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(AppiumeWebModule).Assembly, "dewey")
                .Build();

            Configuration.Modules.ApmWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));

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
        }
    }
}
