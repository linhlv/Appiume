using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Application.Features;
using Appiume.Apm.Dependency;

namespace Appiume.Apm.Authorization
{
    /// <summary>
    /// Permission dependency context.
    /// </summary>
    public interface IPermissionDependencyContext
    {
        /// <summary>
        /// The user which requires permission. Can be null if no user.
        /// </summary>
        UserIdentifier User { get; }

        /// <summary>
        /// Gets the <see cref="IIocResolver"/>.
        /// </summary>
        /// <value>
        /// The ioc resolver.
        /// </value>
        IIocResolver IocResolver { get; }

        /// <summary>
        /// Gets the <see cref="IFeatureChecker"/>.
        /// </summary>
        /// <value>
        /// The feature checker.
        /// </value>
        IPermissionChecker PermissionChecker { get; }
    }
}
