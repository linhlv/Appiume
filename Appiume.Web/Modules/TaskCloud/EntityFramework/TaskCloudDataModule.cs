using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Appiume.Apm.Ef;
using Appiume.Apm.Modules;
using Appiume.Web.Modules.TaskCloud.Core;

namespace Appiume.Web.Modules.TaskCloud.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(TaskCloudCoreModule), typeof(ApmEntityFrameworkModule))]
    public class TaskCloudDataModule : ApmModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}