using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Appiume.Apm.Dependency;
using Appiume.Apm.Modules;
using Appiume.Apm.Tenancy.Ef;
using Appiume.Web.IoT.Core;

namespace Appiume.Web.IoT.Ef
{

    [DependsOn(typeof(ApmTenancyEntityFrameworkModule), typeof(IoTCoreModule))]
    public class IoTDataModule : ApmModule
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