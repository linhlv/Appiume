using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Optimization;
using Appiume.Apm.Application.Services;
using Appiume.Apm.Localization;
using Appiume.Apm.Localization.Dictionaries;
using Appiume.Apm.Localization.Dictionaries.Xml;
using Appiume.Apm.Modules;
using Appiume.Apm.Web.WebApi;
using Appiume.Apm.Web.WebApi.Configuration.Startup;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic.Builders;
using Appiume.Web.Modules.TaskCloud.Application;
using Appiume.Web.Modules.TaskCloud.EntityFramework;
using Appiume.Web.Modules.TaskCloud.WebApi.Navigation;

namespace Appiume.Web.Modules.TaskCloud.WebApi
{
    /// <summary>
    /// 'Web API layer module' for this project.
    /// </summary>
    [DependsOn(typeof(TaskCloudDataModule), typeof(ApmWebApiModule), typeof(TaskCloudApplicationModule))]
    public class TaskCloudWebApiModule : ApmModule
    {
        public override void PreInitialize()
        {
            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("tr", "Türkçe", "famfamfam-flag-tr"));
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", "famfamfam-flag-cn"));

            //Add a localization source
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    "TaskCloud",
                    new XmlFileLocalizationDictionaryProvider(
                        HttpContext.Current.Server.MapPath("~/Modules/TaskCloud/Core/Localization")
                        )
                    )
                );

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<TaskCloudNavigationProvider>();
        }


        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(TaskCloudWebApiModule).Assembly, "taskcloud")
                .Build();

            Configuration.Modules.ApmWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}