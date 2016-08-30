using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Web.Models;

namespace Appiume.Apm.Web.Mvc.Configuration
{
    public interface IApmMvcConfiguration
    {
        UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        WrapResultAttribute DefaultWrapResultAttribute { get; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool IsValidationEnabledForControllers { get; set; }
    }
}
