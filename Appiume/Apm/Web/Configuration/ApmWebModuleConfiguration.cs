using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Web.WebApi.ProxyScripting.Configuration;

namespace Appiume.Apm.Web.Configuration
{
    internal class ApmWebModuleConfiguration : IApmWebModuleConfiguration
    {
        public bool SendAllExceptionsToClients { get; set; }

        public IApiProxyScriptingConfiguration ApiProxyScripting { get; }

        public ApmWebModuleConfiguration(IApiProxyScriptingConfiguration apiProxyScripting)
        {
            ApiProxyScripting = apiProxyScripting;
        }
    }
}