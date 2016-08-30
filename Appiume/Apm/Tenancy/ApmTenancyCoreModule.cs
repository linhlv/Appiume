using System.Linq;
using System.Reflection;
using Appiume.Apm.Tenancy.Application.Features;
using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Apm.Tenancy.Localization;
using Appiume.Apm.Tenancy.MultiTenancy;
using Appiume.Apm.Tenancy.Configuration;
using Castle.MicroKernel.Registration;
using Appiume.Apm.Dependency;
using Appiume.Apm.Localization.Dictionaries;
using Appiume.Apm.Localization.Dictionaries.Xml;
using Appiume.Apm.Modules;
using Appiume.Apm.Reflection;

namespace Appiume.Apm.Tenancy
{
    /// <summary>
    /// Apm tenancy core module.
    /// </summary>
    [DependsOn(typeof(ApmKernelModule))]
    public class ApmTenancyCoreModule : ApmModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IRoleManagementConfig, RoleManagementConfig>();
            IocManager.Register<IUserManagementConfig, UserManagementConfig>();
            IocManager.Register<ILanguageManagementConfig, LanguageManagementConfig>();
            IocManager.Register<IApmTenancyEntityTypes, ApmTenancyEntityTypes>();
            IocManager.Register<IApmTenancyConfig, ApmTenancyConfig>();

            Configuration.Settings.Providers.Add<ApmTenancySettingProvider>();

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    ApmTenancyConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), "Appiume.Apm.Tenancy.Localization.Source"
                        )));

            IocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        public override void Initialize()
        {
            FillMissingEntityTypes();

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.Register<IMultiTenantLocalizationDictionary, MultiTenantLocalizationDictionary>(DependencyLifeStyle.Transient); //could not register conventionally
            RegisterTenantCache();
        }

        private void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {
            if (typeof(IApmTenancyFeatureValueStore).IsAssignableFrom(handler.ComponentModel.Implementation) && !IocManager.IsRegistered<IApmTenancyFeatureValueStore>())
            {
                IocManager.IocContainer.Register(
                    Component.For<IApmTenancyFeatureValueStore>().ImplementedBy(handler.ComponentModel.Implementation).Named("ApmTenancyFeatureValueStore").LifestyleTransient()
                    );
            }
        }

        private void FillMissingEntityTypes()
        {
            using (var entityTypes = IocManager.ResolveAsDisposable<IApmTenancyEntityTypes>())
            {
                if (entityTypes.Object.User != null &&
                    entityTypes.Object.Role != null &&
                    entityTypes.Object.Tenant != null)
                {
                    return;
                }

                using (var typeFinder = IocManager.ResolveAsDisposable<ITypeFinder>())
                {
                    var types = typeFinder.Object.FindAll();
                    entityTypes.Object.Tenant = types.FirstOrDefault(t => typeof(ApmTenantBase).IsAssignableFrom(t) && !t.IsAbstract);
                    entityTypes.Object.Role = types.FirstOrDefault(t => typeof(ApmRoleBase).IsAssignableFrom(t) && !t.IsAbstract);
                    entityTypes.Object.User = types.FirstOrDefault(t => typeof(ApmUserBase).IsAssignableFrom(t) && !t.IsAbstract);
                }
            }
        }

        private void RegisterTenantCache()
        {
            if (IocManager.IsRegistered<ITenantCache>())
            {
                return;
            }

            using (var entityTypes = IocManager.ResolveAsDisposable<IApmTenancyEntityTypes>())
            {
                var implType = typeof (TenantCache<,>)
                    .MakeGenericType(entityTypes.Object.Tenant, entityTypes.Object.User);

                IocManager.Register(typeof (ITenantCache), implType, DependencyLifeStyle.Transient);
            }
        }
    }
}
