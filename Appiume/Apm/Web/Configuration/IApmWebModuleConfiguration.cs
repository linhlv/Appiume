using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Web.WebApi.ProxyScripting.Configuration;

namespace Appiume.Apm.Web.Configuration
{
    /// <summary>
    /// Used to configure Apm Web module.
    /// </summary>
    public interface IApmWebModuleConfiguration
    {
        /// <summary>
        /// If this is set to true, all exception and details are sent directly to clients on an error.
        /// Default: false (Apm hides exception details from clients except special exceptions.)
        /// </summary>
        bool SendAllExceptionsToClients { get; set; }

        /// <summary>
        /// Used to configure Api proxy scripting.
        /// </summary>
        IApiProxyScriptingConfiguration ApiProxyScripting { get; }
    }
}
