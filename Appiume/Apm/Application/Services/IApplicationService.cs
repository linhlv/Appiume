using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Dependency;

namespace Appiume.Apm.Application.Services
{

    /// <summary>
    /// This interface must be implemented by all application services to identify them by convention.
    /// </summary>
    public interface IApplicationService : ITransientDependency
    {

    }
}