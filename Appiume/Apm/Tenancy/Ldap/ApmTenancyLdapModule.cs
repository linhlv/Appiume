using System.Reflection;
using Appiume.Apm.Localization.Dictionaries.Xml;
using Appiume.Apm.Localization.Sources;
using Appiume.Apm.Modules;
using Appiume.Apm.Tenancy.Ldap.Configuration;

namespace Appiume.Apm.Tenancy.Ldap
{
    /// <summary>
    /// This module extends module zero to add LDAP authentication.
    /// </summary>
    [DependsOn(typeof (ApmTenancyCoreModule))]
    public class ApmTenancyLdapModule : ApmModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IApmTenancyLdapModuleConfig, ApmTenancyLdapModuleConfig>();

            Configuration.Localization.Sources.Extensions.Add(
                new LocalizationSourceExtensionInfo(
                    ApmTenancyConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "Appiume.Apm.Tenancy.Ldap.Ldap.Localization.Source")
                    )
                );

            Configuration.Settings.Providers.Add<LdapSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
