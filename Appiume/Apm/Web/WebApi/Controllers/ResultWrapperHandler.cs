using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Appiume.Apm.Dependency;
using Appiume.Apm.Web.Models;
using Appiume.Apm.Web.WebApi.Configuration;

namespace Appiume.Apm.Web.WebApi.Controllers
{
    /// <summary>
    /// Wraps Web API return values by <see cref="AjaxResponse"/>.
    /// </summary>
    public class ResultWrapperHandler : DelegatingHandler, ITransientDependency
    {
        private readonly IApmWebApiConfiguration _webApiConfiguration;

        public ResultWrapperHandler(IApmWebApiConfiguration webApiConfiguration)
        {
            _webApiConfiguration = webApiConfiguration;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith(
                task =>
                {
                    WrapResultIfNeeded(request, task.Result);
                    return task.Result;

                }, cancellationToken);
        }

        protected virtual void WrapResultIfNeeded(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            var wrapAttr = HttpActionDescriptorHelper.GetWrapResultAttributeOrNull(request.GetActionDescriptor())
                           ?? _webApiConfiguration.DefaultWrapResultAttribute;

            if (!wrapAttr.WrapOnSuccess)
            {
                return;
            }

            object resultObject;
            if (!response.TryGetContentValue(out resultObject) || resultObject == null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ObjectContent<AjaxResponse>(
                    new AjaxResponse(),
                    _webApiConfiguration.HttpConfiguration.Formatters.JsonFormatter
                    );
                return;
            }

            if (resultObject is AjaxResponseBase)
            {
                return;
            }

            response.Content = new ObjectContent<AjaxResponse>(
                new AjaxResponse(resultObject),
                _webApiConfiguration.HttpConfiguration.Formatters.JsonFormatter
                );
        }
    }
}