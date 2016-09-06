using System.Web.OData.Builder;

namespace Appiume.Apm.Web.WebApi.OData.Configuration
{
    /// <summary>
    /// Used to configure Appiume.Apm.Web.WebApi.OData module.
    /// </summary>
    public interface IApmWebApiODataModuleConfiguration
    {
        /// <summary>
        /// Gets ODataConventionModelBuilder.
        /// </summary>
        ODataConventionModelBuilder ODataModelBuilder { get; }
    }
}