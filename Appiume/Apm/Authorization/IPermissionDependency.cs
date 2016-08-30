using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Authorization
{
    /// <summary>
    /// Defines interface to check a dependency.
    /// </summary>
    public interface IPermissionDependency
    {
        /// <summary>
        /// Checks if permission dependency is satisfied.
        /// </summary>
        /// <param name="context">Context.</param>
        Task<bool> IsSatisfiedAsync(IPermissionDependencyContext context);
    }
}
