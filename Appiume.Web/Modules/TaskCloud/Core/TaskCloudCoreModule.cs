using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Appiume.Apm.Modules;

namespace Appiume.Web.Modules.TaskCloud.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskCloudCoreModule : ApmModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}