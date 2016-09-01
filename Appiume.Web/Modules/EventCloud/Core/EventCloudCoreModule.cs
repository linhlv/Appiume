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
using Appiume.Web.Modules.EventCloud.Core.Authorization.Roles;
using Appiume.Web.Modules.EventCloud.Core.Configuration;

namespace Appiume.Web.Modules.EventCloud.Core
{
    [DependsOn(typeof(ApmTenancyCoreModule))]
    public class EventCloudCoreModule : ApmModule
    {
        public override void PreInitialize()
        {
            Configuration.MultiTenancy.IsEnabled = true;

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    EventCloudConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Appiume.Web.Modules.EventCloud.Core.Localization.Source"
                        )
                    )
                );

            Configuration.Settings.Providers.Add<EventCloudSettingProvider>();

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