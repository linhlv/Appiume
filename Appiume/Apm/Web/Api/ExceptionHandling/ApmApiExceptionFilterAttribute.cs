using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Appiume.Apm.Dependency;
using Appiume.Apm.Events.Bus;
using Appiume.Apm.Events.Bus.Exceptions;
using Appiume.Apm.Logging;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.Web.Models;
using Appiume.Apm.Web.WebApi.Configuration;
using Appiume.Apm.Web.WebApi.Controllers;
using Castle.Core.Logging;

namespace Appiume.Apm.Web.WebApi.ExceptionHandling
{
    /// <summary>
    /// Used to handle exceptions on web api controllers.
    /// </summary>
    public class ApmApiExceptionFilterAttribute : ExceptionFilterAttribute, ITransientDependency
    {
        /// <summary>
        /// Reference to the <see cref="ILogger"/>.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Reference to the <see cref="IEventBus"/>.
        /// </summary>
        public IEventBus EventBus { get; set; }

        public IApmSession ApmSession { get; set; }

        private readonly IApmWebApiConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApmApiExceptionFilterAttribute"/> class.
        /// </summary>
        public ApmApiExceptionFilterAttribute(IApmWebApiConfiguration configuration)
        {
            _configuration = configuration;
            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
            ApmSession = NullApmSession.Instance;
        }

        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="context">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var wrapResultAttribute = HttpActionDescriptorHelper
                .GetWrapResultAttributeOrNull(context.ActionContext.ActionDescriptor) ??
                _configuration.DefaultWrapResultAttribute;

            if (wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            if (wrapResultAttribute.WrapOnError)
            {
                context.Response = context.Request.CreateResponse(
                    GetStatusCode(context),
                    new AjaxResponse(
                        SingletonDependency<ErrorInfoBuilder>.Instance.BuildForException(context.Exception),
                        context.Exception is Appiume.Apm.Authorization.ApmAuthorizationException)
                    );

                EventBus.Trigger(this, new ApmHandledExceptionData(context.Exception));
            }
        }

        private HttpStatusCode GetStatusCode(HttpActionExecutedContext context)
        {
            if (context.Exception is Appiume.Apm.Authorization.ApmAuthorizationException)
            {
                return ApmSession.UserId.HasValue
                    ? HttpStatusCode.Forbidden
                    : HttpStatusCode.Unauthorized;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}