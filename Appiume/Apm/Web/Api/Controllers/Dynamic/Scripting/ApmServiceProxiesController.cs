using System.Net.Http;
using System.Net.Http.Headers;
using Appiume.Apm.Auditing;
using Appiume.Apm.Web.Models;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic.Formatters;

namespace Appiume.Apm.Web.WebApi.Controllers.Dynamic.Scripting
{
    /// <summary>
    /// This class is used to create proxies to call dynamic api methods from Javascript clients.
    /// </summary>
    [DontWrapResult]
    [DisableAuditing]
    public class ApmServiceProxiesController : ApmApiController
    {
        private readonly ScriptProxyManager _scriptProxyManager;

        public ApmServiceProxiesController(ScriptProxyManager scriptProxyManager)
        {
            _scriptProxyManager = scriptProxyManager;
        }

        /// <summary>
        /// Gets javascript proxy for given service name.
        /// </summary>
        /// <param name="name">Name of the service</param>
        /// <param name="type">Script type</param>
        public HttpResponseMessage Get(string name, ProxyScriptType type = ProxyScriptType.JQuery)
        {
            var script = _scriptProxyManager.GetScript(name, type);
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, script, new PlainTextFormatter());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-javascript");
            return response;
        }

        /// <summary>
        /// Gets javascript proxy for all services.
        /// </summary>
        /// <param name="type">Script type</param>
        public HttpResponseMessage GetAll(ProxyScriptType type = ProxyScriptType.JQuery)
        {
            var script = _scriptProxyManager.GetAllScript(type);
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, script, new PlainTextFormatter());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-javascript");
            return response;
        }
    }
}