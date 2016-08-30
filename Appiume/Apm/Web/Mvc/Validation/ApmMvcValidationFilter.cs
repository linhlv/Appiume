using System.Web.Mvc;
using Appiume.Apm.Dependency;
using Appiume.Apm.Web.Mvc.Configuration;
using Appiume.Apm.Web.Mvc.Extensions;

namespace Appiume.Apm.Web.Mvc.Validation
{
    public class ApmMvcValidationFilter : IActionFilter, ITransientDependency
    {
        private readonly IIocResolver _iocResolver;
        private readonly IApmMvcConfiguration _configuration;

        public ApmMvcValidationFilter(IIocResolver iocResolver, IApmMvcConfiguration configuration)
        {
            _iocResolver = iocResolver;
            _configuration = configuration;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!_configuration.IsValidationEnabledForControllers)
            {
                return;
            }

            var methodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return;
            }

            using (var validator = _iocResolver.ResolveAsDisposable<MvcActionInvocationValidator>())
            {
                validator.Object.Initialize(filterContext, methodInfo);
                validator.Object.Validate();
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}
