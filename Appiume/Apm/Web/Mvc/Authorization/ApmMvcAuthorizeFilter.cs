using System.Net;
using System.Reflection;
using System.Web.Mvc;
using Appiume.Apm.Auditing;
using Appiume.Apm.Authorization;
using Appiume.Apm.Dependency;
using Appiume.Apm.Logging;
using Appiume.Apm.Web.Models;
using Appiume.Apm.Web.Mvc.Controllers.Results;
using Appiume.Apm.Web.Mvc.Extensions;
using Appiume.Apm.Web.Mvc.Helpers;

namespace Appiume.Apm.Web.Mvc.Authorization
{
    public class ApmMvcAuthorizeFilter : IAuthorizationFilter, ITransientDependency
    {
        private readonly IAuthorizationHelper _authorizationHelper;
        private readonly IErrorInfoBuilder _errorInfoBuilder;

        public ApmMvcAuthorizeFilter(IAuthorizationHelper authorizationHelper, IErrorInfoBuilder errorInfoBuilder)
        {
            _authorizationHelper = authorizationHelper;
            _errorInfoBuilder = errorInfoBuilder;
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            var methodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return;
            }

            try
            {
                _authorizationHelper.Authorize(methodInfo);
            }
            catch (ApmAuthorizationException ex)
            {
                LogHelper.Logger.Warn(ex.ToString(), ex);
                HandleUnauthorizedRequest(filterContext, methodInfo, ex);
            }
        }

        protected virtual void HandleUnauthorizedRequest(
            AuthorizationContext filterContext, 
            MethodInfo methodInfo, 
            ApmAuthorizationException ex)
        {
            filterContext.HttpContext.Response.StatusCode =
                filterContext.RequestContext.HttpContext.User?.Identity?.IsAuthenticated ?? false
                    ? (int) HttpStatusCode.Forbidden
                    : (int) HttpStatusCode.Unauthorized;

            var isJsonResult = MethodInfoHelper.IsJsonResult(methodInfo);

            if (isJsonResult)
            {
                filterContext.Result = CreateUnAuthorizedJsonResult(ex);
            }
            else
            {
                filterContext.Result = CreateUnAuthorizedNonJsonResult(filterContext, ex);
            }

            if (isJsonResult || filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
            }
        }

        protected virtual ApmJsonResult CreateUnAuthorizedJsonResult(ApmAuthorizationException ex)
        {
            return new ApmJsonResult(
                new AjaxResponse(_errorInfoBuilder.BuildForException(ex), true))
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
        }

        protected virtual HttpStatusCodeResult CreateUnAuthorizedNonJsonResult(AuthorizationContext filterContext, ApmAuthorizationException ex)
        {
            return new HttpStatusCodeResult(filterContext.HttpContext.Response.StatusCode, ex.Message);
        }
    }
}