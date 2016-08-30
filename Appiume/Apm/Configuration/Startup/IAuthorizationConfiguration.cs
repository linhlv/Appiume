using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Authorization;
using Appiume.Apm.Collections;

namespace Appiume.Apm.Configuration.Startup
{
    /// <summary>
    /// Used to configure authorization system.
    /// </summary>
    public interface IAuthorizationConfiguration
    {
        /// <summary>
        /// List of authorization providers.
        /// </summary>
        ITypeList<AuthorizationProvider> Providers { get; }
    }
}
