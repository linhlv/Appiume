using System.Web.Http;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Web.Models;

namespace Appiume.Apm.Web.WebApi.Configuration
{
    /// <summary>
    /// Used to configure ABP WebApi module.
    /// </summary>
    public interface IApmWebApiConfiguration
    {
        UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        WrapResultAttribute DefaultWrapResultAttribute { get; }

        WrapResultAttribute DefaultDynamicApiWrapResultAttribute { get; }

        /// <summary>
        /// Gets/sets <see cref="HttpConfiguration"/>.
        /// </summary>
        HttpConfiguration HttpConfiguration { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        bool IsValidationEnabledForControllers { get; set; }
    }
}