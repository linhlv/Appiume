using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Appiume.Apm.Application.Services;
using Appiume.Apm.Modules;
using Appiume.Apm.Web.WebApi;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic.Builders;
using Appiume.Web.Modules.TaskCloud.Application;

namespace Appiume.Web.Modules.TaskCloud.WebApi
{
    /// <summary>
    /// 'Web API layer module' for this project.
    /// </summary>
    [DependsOn(typeof(ApmWebApiModule), typeof(TaskCloudApplicationModule))]
    public class TaskCloudWebApiModule : ApmModule
    {
        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Creating dynamic Web Api Controllers for application services.
            //Thus, 'web api layer' is created automatically by ABP.

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(Assembly.GetAssembly(typeof(TaskCloudApplicationModule)), "taskcloud")
                .Build();
        }
    }
}