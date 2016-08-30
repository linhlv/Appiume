using System.Web;
using System.Web.Mvc;
using Appiume.Apm.Dependency;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Web.Mvc.Configuration;
using Appiume.Apm.Web.Mvc.Extensions;

namespace Appiume.Apm.Web.Mvc.Uow
{
    public class ApmMvcUowFilter: IActionFilter, ITransientDependency
    {
        public const string UowHttpContextKey = "__ApmUnitOfWork";

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IApmMvcConfiguration _configuration;

        public ApmMvcUowFilter(
            IUnitOfWorkManager unitOfWorkManager,
            IApmMvcConfiguration configuration)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _configuration = configuration;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }

            var methodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            if (methodInfo == null)
            {
                return;
            }

            var unitOfWorkAttr = UnitOfWorkAttribute.GetUnitOfWorkAttributeOrNull(methodInfo) ??
                                 _configuration.DefaultUnitOfWorkAttribute;

            if (unitOfWorkAttr.IsDisabled)
            {
                return;
            }

            SetCurrentUow(
                filterContext.HttpContext,
                _unitOfWorkManager.Begin(unitOfWorkAttr.CreateOptions())
            );
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }

            var uow = GetCurrentUow(filterContext.HttpContext);
            if (uow == null)
            {
                return;
            }

            try
            {
                if (filterContext.Exception == null)
                {
                    uow.Complete();
                }
            }
            finally
            {
                uow.Dispose();
                SetCurrentUow(filterContext.HttpContext, null);
            }
        }

        private static IUnitOfWorkCompleteHandle GetCurrentUow(HttpContextBase httpContext)
        {
            return httpContext.Items[UowHttpContextKey] as IUnitOfWorkCompleteHandle;
        }

        private static void SetCurrentUow(HttpContextBase httpContext, IUnitOfWorkCompleteHandle uow)
        {
            httpContext.Items[UowHttpContextKey] = uow;
        }
    }
}
