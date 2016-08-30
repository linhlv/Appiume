using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Appiume.Apm.Localization.Dictionaries;
using Appiume.Apm.Localization.Dictionaries.Xml;
using Appiume.Apm.Modules;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Tenancy;
using Appiume.Apm.Tenancy.Configuration;
using Appiume.Apm.Timing;
using Appiume.Web.IoT.Core.Authorization.Roles;
using Appiume.Web.IoT.Core.Configuration;

namespace Appiume.Web.IoT.Core
{
    [DependsOn(typeof(ApmTenancyCoreModule))]
    public class IoTCoreModule : ApmModule
    {
        public override void PreInitialize()
        {
            Configuration.MultiTenancy.IsEnabled = true;

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    IoTConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Appiume.Web.IoT.Localization.Source"
                        )
                    )
                );

            Configuration.Settings.Providers.Add<IoTSettingProvider>();

            Configuration.Modules.Tenancy().RoleManagement.StaticRoles.Add(new StaticRoleDefinition(StaticRoleNames.Tenant.Admin, MultiTenancySides.Host));
            Configuration.Modules.Tenancy().RoleManagement.StaticRoles.Add(new StaticRoleDefinition(StaticRoleNames.Tenant.Admin, MultiTenancySides.Tenant));
            Configuration.Modules.Tenancy().RoleManagement.StaticRoles.Add(new StaticRoleDefinition(StaticRoleNames.Tenant.Member, MultiTenancySides.Tenant));

            Clock.Provider = new UtcClockProvider();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}