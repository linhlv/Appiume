using System.Web.Http;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Web.Models;

namespace Appiume.Apm.Web.WebApi.Configuration
{
    internal class ApmWebApiConfiguration : IApmWebApiConfiguration
    {
        public UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        public WrapResultAttribute DefaultWrapResultAttribute { get; }

        public WrapResultAttribute DefaultDynamicApiWrapResultAttribute { get; }

        public HttpConfiguration HttpConfiguration { get; set; }

        public bool IsValidationEnabledForControllers { get; set; }

        public ApmWebApiConfiguration()
        {
            HttpConfiguration = GlobalConfiguration.Configuration;
            DefaultUnitOfWorkAttribute = new UnitOfWorkAttribute();
            DefaultWrapResultAttribute = new WrapResultAttribute(false);
            DefaultDynamicApiWrapResultAttribute = new WrapResultAttribute();
            IsValidationEnabledForControllers = true;
        }
    }
}