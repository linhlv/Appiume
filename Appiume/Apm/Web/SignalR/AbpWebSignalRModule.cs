using System.Reflection;
using Appiume.Apm.Modules;
using Castle.MicroKernel.Registration;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace Appiume.Apm.Web.SignalR
{
    /// <summary>
    /// ABP SignalR integration module.
    /// </summary>
    [DependsOn(typeof(ApmKernelModule))]
    public class ApmWebSignalRModule : ApmModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            GlobalHost.DependencyResolver = new WindsorDependencyResolver(IocManager.IocContainer);
            UseApmSignalRContractResolver();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        private void UseApmSignalRContractResolver()
        {
            var serializer = JsonSerializer.Create(
                new JsonSerializerSettings
                {
                    ContractResolver = new ApmSignalRContractResolver()
                });
            
            IocManager.IocContainer.Register(
                Component.For<JsonSerializer>().UsingFactoryMethod(() => serializer)
                );
        }
    }
}
