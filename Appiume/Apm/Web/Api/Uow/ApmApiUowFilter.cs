using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Appiume.Apm.Dependency;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Web.WebApi.Configuration;
using Appiume.Apm.Web.WebApi.Validation;

namespace Appiume.Apm.Web.WebApi.Uow
{
    public class ApmApiUowFilter : IActionFilter, ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IApmWebApiConfiguration _configuration;

        public bool AllowMultiple => false;

        public ApmApiUowFilter(
            IUnitOfWorkManager unitOfWorkManager,
            IApmWebApiConfiguration configuration
            )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
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

            var unitOfWorkAttr = UnitOfWorkAttribute.GetUnitOfWorkAttributeOrNull(methodInfo) ??
                                 _configuration.DefaultUnitOfWorkAttribute;

            if (unitOfWorkAttr.IsDisabled)
            {
                return await continuation();
            }

            using (var uow = _unitOfWorkManager.Begin(unitOfWorkAttr.CreateOptions()))
            {
                var result = await continuation();
                await uow.CompleteAsync();
                return result;
            }
        }
    }
}