using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Appiume.Apm.Authorization;
using Appiume.Apm.Dependency;
using Appiume.Apm.Logging;
using Appiume.Apm.Web.WebApi.Validation;

namespace Appiume.Apm.Web.WebApi.Authorization
{
    public class ApmApiAuthorizeFilter : IAuthorizationFilter, ITransientDependency
    {
        public bool AllowMultiple => false;

        private readonly IAuthorizationHelper _authorizationHelper;

        public ApmApiAuthorizeFilter(IAuthorizationHelper authorizationHelper)
        {
            _authorizationHelper = authorizationHelper;
        }

        public virtual async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            var methodInfo = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return await continuation();
            }

            if (actionContext.ActionDescriptor.IsDynamicApmAction())
            {
                return await continuation();
            }

            try
            {
                await _authorizationHelper.AuthorizeAsync(methodInfo);
                return await continuation();
            }
            catch (ApmAuthorizationException ex)
            {
                LogHelper.Logger.Warn(ex.ToString(), ex);
                return CreateUnAuthorizedResponse(actionContext);
            }
        }

        protected virtual HttpResponseMessage CreateUnAuthorizedResponse(HttpActionContext actionContext)
        {
            var response = new HttpResponseMessage(
                actionContext.RequestContext.Principal?.Identity?.IsAuthenticated ?? false
                    ? HttpStatusCode.Forbidden
                    : HttpStatusCode.Unauthorized
            );

            return response;
        }
    }
}