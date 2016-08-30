using System.Reflection;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Dependency;
using Appiume.Apm.Ef.Common;
using Appiume.Apm.Ef.Repositories;
using Appiume.Apm.Ef.Uow;
using Appiume.Apm.Modules;
using Appiume.Apm.Reflection;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;

namespace Appiume.Apm.Ef
{
    /// <summary>
    /// This module is used to implement "Data Access Layer" in EntityFramework.
    /// </summary>
    [DependsOn(typeof(ApmKernelModule))]
    public class ApmEntityFrameworkModule : ApmModule
    {
        public ILogger Logger { get; set; }

        private readonly ITypeFinder _typeFinder;

        public ApmEntityFrameworkModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
            Logger = NullLogger.Instance;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
                Component.For(typeof(IDbContextProvider<>))
                    .ImplementedBy(typeof(UnitOfWorkDbContextProvider<>))
                    .LifestyleTransient()
                );

            RegisterGenericRepositoriesAndMatchDbContexes();
        }

        private void RegisterGenericRepositoriesAndMatchDbContexes()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                    type.IsPublic &&
                    !type.IsAbstract &&
                    type.IsClass &&
                    typeof(ApmDbContext).IsAssignableFrom(type)
                    );

            if (dbContextTypes.IsNullOrEmpty())
            {
                Logger.Warn("No class found derived from ApmDbContext.");
                return;
            }

            using (var repositoryRegistrar = IocManager.ResolveAsDisposable<EntityFrameworkGenericRepositoryRegistrar>())
            {
                foreach (var dbContextType in dbContextTypes)
                {
                    Logger.Debug("Registering DbContext: " + dbContextType.AssemblyQualifiedName);
                    repositoryRegistrar.Object.RegisterForDbContext(dbContextType, IocManager);
                }
            }

            using (var dbContextMatcher = IocManager.ResolveAsDisposable<IDbContextTypeMatcher>())
            {
                dbContextMatcher.Object.Populate(dbContextTypes);
            }
        }
    }
}
