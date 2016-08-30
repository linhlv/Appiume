using System.Reflection;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Modules;
using Appiume.Apm.MultiTenancy;
using Castle.MicroKernel.Registration;
using Appiume.Apm.Ef;
using Appiume.Apm.Tenancy.MultiTenancy;

namespace Appiume.Apm.Tenancy.Ef
{
    /// <summary>
    /// Entity framework integration module for ASP.NET Boilerplate Zero.
    /// </summary>
    [DependsOn(typeof(ApmTenancyCoreModule), typeof(ApmEntityFrameworkModule))]
    public class ApmTenancyEntityFrameworkModule : ApmModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService(typeof(IConnectionStringResolver), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IConnectionStringResolver, IDbPerTenantConnectionStringResolver>()
                        .ImplementedBy<DbPerTenantConnectionStringResolver>()
                        .LifestyleTransient()
                    );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
