using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Appiume.Apm.Dependency;
using Appiume.Apm.Modules;
using Appiume.Apm.Tenancy.Ef;
using Appiume.Web.Modules.EventCloud.Core;

namespace Appiume.Web.Modules.EventCloud.EntityFramework
{

    [DependsOn(typeof(ApmTenancyEntityFrameworkModule), typeof(EventCloudCoreModule))]
    public class EventCloudDataModule : ApmModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}