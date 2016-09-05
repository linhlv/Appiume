using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Appiume.Apm.AutoMapper;
using Appiume.Apm.Modules;
using Appiume.Web.Modules.TaskCloud.Core;

namespace Appiume.Web.Modules.TaskCloud.Application
{
    /// <summary>
    /// 'Application layer module' for this project.
    /// </summary>
    [DependsOn(typeof(TaskCloudCoreModule), typeof(ApmAutoMapperModule))]
    public class TaskCloudApplicationModule : ApmModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //We must declare mappings to be able to use AutoMapper
            DtoMappings.Map();
        }
    }
}