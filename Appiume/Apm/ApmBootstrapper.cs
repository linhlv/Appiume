using System;
using System.Collections.Generic;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;
using Appiume.Apm.Dependency.Installers;
using Appiume.Apm.Modules;
using Appiume.Apm.PlugIns;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using JetBrains.Annotations;

namespace Appiume.Apm
{
    /// <summary>
    /// This is the main class that is responsible to start entire Apm system.
    /// Prepares dependency injection and registers core components needed for startup.
    /// It must be instantiated and initialized (see <see cref="Initialize"/>) first in an application.
    /// </summary>
    public class ApmBootstrapper : IDisposable
    {
        /// <summary>
        /// Get the startup module of the application which depends on other used modules.
        /// </summary>
        public Type StartupModule { get; }

        /// <summary>
        /// A list of plug in folders.
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        /// <summary>
        /// Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        /// Is this object disposed before?
        /// </summary>
        protected bool IsDisposed;

        private ApmModuleManager _moduleManager;
        private ILogger _logger;

        /// <summary>
        /// Creates a new <see cref="ApmBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="ApmModule"/>.</param>
        private ApmBootstrapper([NotNull] Type startupModule)
            : this(startupModule, Dependency.IocManager.Instance)
        {

        }

        /// <summary>
        /// Creates a new <see cref="ApmBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="ApmModule"/>.</param>
        /// <param name="iocManager">IIocManager that is used to bootstrap the Apm system</param>
        private ApmBootstrapper([NotNull] Type startupModule, [NotNull] IIocManager iocManager)
        {
            Check.NotNull(startupModule, nameof(startupModule));
            Check.NotNull(iocManager, nameof(iocManager));

            if (!typeof(ApmModule).IsAssignableFrom(startupModule))
            {
                throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(ApmModule)}.");
            }

            StartupModule = startupModule;
            IocManager = iocManager;

            PlugInSources = new PlugInSourceList();
            _logger = NullLogger.Instance;
        }

        /// <summary>
        /// Creates a new <see cref="ApmBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="ApmModule"/>.</typeparam>
        public static ApmBootstrapper Create<TStartupModule>()
            where TStartupModule : ApmModule
        {
            return new ApmBootstrapper(typeof(TStartupModule));
        }

        /// <summary>
        /// Creates a new <see cref="ApmBootstrapper"/> instance.
        /// </summary>
        /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="ApmModule"/>.</typeparam>
        /// <param name="iocManager">IIocManager that is used to bootstrap the Apm system</param>
        public static ApmBootstrapper Create<TStartupModule>([NotNull] IIocManager iocManager)
            where TStartupModule : ApmModule
        {
            return new ApmBootstrapper(typeof(TStartupModule), iocManager);
        }

        /// <summary>
        /// Creates a new <see cref="ApmBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="ApmModule"/>.</param>
        public static ApmBootstrapper Create([NotNull] Type startupModule)
        {
            return new ApmBootstrapper(startupModule);
        }

        /// <summary>
        /// Creates a new <see cref="ApmBootstrapper"/> instance.
        /// </summary>
        /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="ApmModule"/>.</param>
        /// <param name="iocManager">IIocManager that is used to bootstrap the Apm system</param>
        public static ApmBootstrapper Create([NotNull] Type startupModule, [NotNull] IIocManager iocManager)
        {
            return new ApmBootstrapper(startupModule, iocManager);
        }

        /// <summary>
        /// Initializes the Apm system.
        /// </summary>
        public virtual void Initialize()
        {
            ResolveLogger();

            try
            {
                RegisterBootstrapper();
                IocManager.IocContainer.Install(new ApmCoreInstaller());

                IocManager.Resolve<ApmPlugInManager>().PlugInSources.AddRange(PlugInSources);
                IocManager.Resolve<ApmStartupConfiguration>().Initialize();

                _moduleManager = IocManager.Resolve<ApmModuleManager>();
                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
                throw;
            }
        }

        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(ApmBootstrapper));
            }
        }

        private void RegisterBootstrapper()
        {
            if (!IocManager.IsRegistered<ApmBootstrapper>())
            {
                IocManager.IocContainer.Register(
                    Component.For<ApmBootstrapper>().Instance(this)
                    );
            }
        }

        /// <summary>
        /// Disposes the APM system.
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            _moduleManager?.ShutdownModules();
        }
    }
}
