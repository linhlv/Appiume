using System;
using System.Collections.Generic;

namespace Appiume.Apm.Web.WebApi.ProxyScripting.Configuration
{
    public interface IApiProxyScriptingConfiguration
    {
        /// <summary>
        /// Used to add/replace proxy script generators. 
        /// </summary>
        IDictionary<string, Type> Generators { get; }
    }
}