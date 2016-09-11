using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Appiume.Apm.Application.Features;
using Appiume.Apm.Application.Navigation;
using Appiume.Apm.Application.Services;
using Appiume.Apm.Auditing;
using Appiume.Apm.Authorization;
using Appiume.Apm.BackgroundJobs;
using Appiume.Apm.Configuration;
using Appiume.Apm.Dependency;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Events.Bus;
using Appiume.Apm.Localization;
using Appiume.Apm.Localization.Dictionaries;
using Appiume.Apm.Localization.Dictionaries.Xml;
using Appiume.Apm.Modules;
using Appiume.Apm.Net.Mail;
using Appiume.Apm.Notifications;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.Runtime.Validation.Interception;
using Appiume.Apm.Timing;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.FirebaseCloudMessaging;
using Appiume.Apm.Runtime.Caching;
using Appiume.Apm.Threading;
using Appiume.Apm.Threading.BackgroundWorkers;
using Castle.MicroKernel.Registration;

namespace Appiume.Apm
{
    /// <summary>
    /// Kernel (core) module of the Apm system.
    /// No need to depend on this, it's automatically the first module always.
    /// </summary>
    public sealed class ApmKernelModule : ApmModule
    {
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

            ValidationInterceptorRegistrar.Initialize(IocManager);
            AuditingInterceptorRegistrar.Initialize(IocManager);
            UnitOfWorkRegistrar.Initialize(IocManager);
            AuthorizationInterceptorRegistrar.Initialize(IocManager);

            Configuration.Auditing.Selectors.Add(
                new NamedTypeSelector(
                    "Apm.ApplicationServices",
                    type => typeof(IApplicationService).IsAssignableFrom(type)
                    )
                );

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    ApmConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), "Apm.Localization.Sources.ApmXmlSource"
                        )));

            Configuration.Settings.Providers.Add<LocalizationSettingProvider>();
            Configuration.Settings.Providers.Add<EmailSettingProvider>();
            Configuration.Settings.Providers.Add<NotificationSettingProvider>();
            Configuration.Settings.Providers.Add<TimingSettingProvider>();

            Configuration.UnitOfWork.RegisterFilter(ApmDataFilters.SoftDelete, true);
            Configuration.UnitOfWork.RegisterFilter(ApmDataFilters.MustHaveTenant, true);
            Configuration.UnitOfWork.RegisterFilter(ApmDataFilters.MayHaveTenant, true);

            ConfigureCaches();
        }

        public override void Initialize()
        {
            foreach (var replaceAction in ((ApmStartupConfiguration)Configuration).ServiceReplaceActions.Values)
            {
                replaceAction();
            }

            IocManager.IocContainer.Install(new EventBusInstaller(IocManager));

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }

        public override void PostInitialize()
        {
            RegisterMissingComponents();

            IocManager.Resolve<SettingDefinitionManager>().Initialize();
            IocManager.Resolve<FeatureManager>().Initialize();
            IocManager.Resolve<PermissionManager>().Initialize();
            IocManager.Resolve<LocalizationManager>().Initialize();
            IocManager.Resolve<NotificationDefinitionManager>().Initialize();
            IocManager.Resolve<NavigationManager>().Initialize();

            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                var workerManager = IocManager.Resolve<IBackgroundWorkerManager>();
                workerManager.Start();
                workerManager.Add(IocManager.Resolve<IBackgroundJobManager>());
            }
        }

        public override void Shutdown()
        {
            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                IocManager.Resolve<IBackgroundWorkerManager>().StopAndWaitToStop();
            }
        }

        private void ConfigureCaches()
        {
            Configuration.Caching.Configure(ApmCacheNames.ApplicationSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(8);
            });

            Configuration.Caching.Configure(ApmCacheNames.TenantSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(60);
            });

            Configuration.Caching.Configure(ApmCacheNames.UserSettings, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(20);
            });
        }

        private void RegisterMissingComponents()
        {
            if (!IocManager.IsRegistered<IGuidGenerator>())
            {
                IocManager.IocContainer.Register(
                    Component
                        .For<IGuidGenerator, SequentialGuidGenerator>()
                        .Instance(SequentialGuidGenerator.Instance)
                );
            }

            IocManager.RegisterIfNot<IUnitOfWork, NullUnitOfWork>(DependencyLifeStyle.Transient);
            IocManager.RegisterIfNot<IAuditInfoProvider, NullAuditInfoProvider>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IAuditingStore, SimpleLogAuditingStore>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IApmSession, ClaimsApmSession>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IPermissionChecker, NullPermissionChecker>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<IRealTimeNotifier, NullRealTimeNotifier>(DependencyLifeStyle.Singleton);
            IocManager.RegisterIfNot<INotificationStore, NullNotificationStore>(DependencyLifeStyle.Singleton);

            IocManager.RegisterIfNot<IBackgroundJobManager, BackgroundJobManager>(DependencyLifeStyle.Singleton);

            IocManager.RegisterIfNot<IFirebaseCloudMessagingNotification, FirebaseCloudMessagingNotification>(DependencyLifeStyle.Singleton);

            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
            {
                IocManager.RegisterIfNot<IBackgroundJobStore, InMemoryBackgroundJobStore>(DependencyLifeStyle.Singleton);
            }
            else
            {
                IocManager.RegisterIfNot<IBackgroundJobStore, NullBackgroundJobStore>(DependencyLifeStyle.Singleton);
            }
        }
    }
}