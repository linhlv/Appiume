using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Web.Models;

namespace Appiume.Apm.Web.Mvc.Configuration
{
    public class ApmMvcConfiguration : IApmMvcConfiguration
    {
        public UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        public WrapResultAttribute DefaultWrapResultAttribute { get; }

        public bool IsValidationEnabledForControllers { get; set; }

        public ApmMvcConfiguration()
        {
            DefaultUnitOfWorkAttribute = new UnitOfWorkAttribute();
            DefaultWrapResultAttribute = new WrapResultAttribute();
            IsValidationEnabledForControllers = true;
        }
    }
}