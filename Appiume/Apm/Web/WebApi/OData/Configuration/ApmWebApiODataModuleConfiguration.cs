using System.Web.OData.Builder;

namespace Appiume.Apm.Web.WebApi.OData.Configuration
{
    internal class ApmWebApiODataModuleConfiguration : IApmWebApiODataModuleConfiguration
    {
        public ODataConventionModelBuilder ODataModelBuilder { get; private set; }

        public ApmWebApiODataModuleConfiguration()
        {
            ODataModelBuilder  = new ODataConventionModelBuilder();
        }
    }
}