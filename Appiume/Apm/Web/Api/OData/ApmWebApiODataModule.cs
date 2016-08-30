using System.Reflection;
using System.Web.OData;
using System.Web.OData.Extensions;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;
using Appiume.Apm.Modules;
using Appiume.Apm.Web.WebApi.Configuration.Startup;
using Appiume.Apm.Web.WebApi.OData.Configuration;

namespace Appiume.Apm.Web.WebApi.OData
{
    [DependsOn(typeof(ApmWebApiModule))]
    public class ApmWebApiODataModule : ApmModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IApmWebApiODataModuleConfiguration, ApmWebApiODataModuleConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.Register<MetadataController>(DependencyLifeStyle.Transient);
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.ApmWebApi().HttpConfiguration.MapODataServiceRoute(
                    routeName: "ODataRoute",
                    routePrefix: "odata",
                    model: Configuration.Modules.ApmWebApiOData().ODataModelBuilder.GetEdmModel()
                );
        }
    }
}
