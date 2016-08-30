using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Appiume.Apm.Dependency;
using Appiume.Apm.Web.WebApi.Configuration;

namespace Appiume.Apm.Web.WebApi.Validation
{
    public class ApmApiValidationFilter : IActionFilter, ITransientDependency
    {
        public bool AllowMultiple => false;

        private readonly IIocResolver _iocResolver;
        private readonly IApmWebApiConfiguration _configuration;

        public ApmApiValidationFilter(IIocResolver iocResolver, IApmWebApiConfiguration configuration)
        {
            _iocResolver = iocResolver;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (!_configuration.IsValidationEnabledForControllers)
            {
                return await continuation();
            }

            var methodInfo = actionContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return await continuation();
            }

            if (actionContext.ActionDescriptor.IsDynamicApmAction())
            {
                return await continuation();
            }

            using (var validator = _iocResolver.ResolveAsDisposable<WebApiActionInvocationValidator>())
            {
                validator.Object.Initialize(actionContext, methodInfo);
                validator.Object.Validate();
            }

            return await continuation();
        }
    }
}