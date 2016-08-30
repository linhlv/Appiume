using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Appiume.Apm.Application.Features;
using Appiume.Apm.Authorization;
using Appiume.Apm.Configuration;
using Appiume.Apm.Dependency;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Events.Bus;
using Appiume.Apm.Events.Bus.Exceptions;
using Appiume.Apm.Localization;
using Appiume.Apm.Localization.Sources;
using Appiume.Apm.Logging;
using Appiume.Apm.Reflection;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.Web.Models;
using Appiume.Apm.Web.Mvc.Configuration;
using Appiume.Apm.Web.Mvc.Controllers.Results;
using Appiume.Apm.Web.Mvc.Extensions;
using Appiume.Apm.Web.Mvc.Helpers;
using Appiume.Apm.Web.Mvc.Models;
using Castle.Core.Logging;

namespace Appiume.Apm.Web.Mvc.Controllers
{
    /// <summary>
    /// Base class for all MVC Controllers in Apm system.
    /// </summary>
    public abstract class ApmController : Controller
    {
        /// <summary>
        /// Gets current session information.
        /// </summary>
        public IApmSession ApmSession { get; set; }

        /// <summary>
        /// Gets the event bus.
        /// </summary>
        public IEventBus EventBus { get; set; }

        /// <summary>
        /// Reference to the permission manager.
        /// </summary>
        public IPermissionManager PermissionManager { get; set; }

        /// <summary>
        /// Reference to the setting manager.
        /// </summary>
        public ISettingManager SettingManager { get; set; }

        /// <summary>
        /// Reference to the permission checker.
        /// </summary>
        public IPermissionChecker PermissionChecker { protected get; set; }

        /// <summary>
        /// Reference to the feature manager.
        /// </summary>
        public IFeatureManager FeatureManager { protected get; set; }

        /// <summary>
        /// Reference to the permission checker.
        /// </summary>
        public IFeatureChecker FeatureChecker { protected get; set; }

        /// <summary>
        /// Reference to the localization manager.
        /// </summary>
        public ILocalizationManager LocalizationManager { protected get; set; }

        /// <summary>
        /// Reference to the error info builder.
        /// </summary>
        public IErrorInfoBuilder ErrorInfoBuilder { protected get; set; }

        /// <summary>
        /// Gets/sets name of the localization source that is used in this application service.
        /// It must be set in order to use <see cref="L(string)"/> and <see cref="L(string,CultureInfo)"/> methods.
        /// </summary>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        /// Gets localization source.
        /// It's valid if <see cref="LocalizationSourceName"/> is set.
        /// </summary>
        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (LocalizationSourceName == null)
                {
                    throw new ApmException("Must set LocalizationSourceName before, in order to get LocalizationSource");
                }

                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
                }

                return _localizationSource;
            }
        }
        private ILocalizationSource _localizationSource;

        /// <summary>
        /// Reference to the logger to write logs.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets current session information.
        /// </summary>
        [Obsolete("Use ApmSession property instead. CurrentSession will be removed in future releases.")]
        protected IApmSession CurrentSession { get { return ApmSession; } }

        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new ApmException("Must set UnitOfWorkManager before use it.");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Gets current unit of work.
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        public IIocResolver IocResolver { get; set; }

        public IApmMvcConfiguration ApmMvcConfiguration { get; set; }

        /// <summary>
        /// MethodInfo for currently executing action.
        /// </summary>
        private MethodInfo _currentMethodInfo;

        /// <summary>
        /// WrapResultAttribute for currently executing action.
        /// </summary>
        private WrapResultAttribute _wrapResultAttribute;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ApmController()
        {
            ApmSession = NullApmSession.Instance;
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
            PermissionChecker = NullPermissionChecker.Instance;
            EventBus = NullEventBus.Instance;
            IocResolver = IocManager.Instance;
        }

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// Gets localized string for given key name and specified culture information.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <returns>Localized string</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// </summary>
        /// <param name="name">Key name</param>
        /// <param name="culture">culture information</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Localized string</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }

        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        protected Task<bool> IsGrantedAsync(string permissionName)
        {
            return PermissionChecker.IsGrantedAsync(permissionName);
        }

        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        protected bool IsGranted(string permissionName)
        {
            return PermissionChecker.IsGranted(permissionName);
        }


        /// <summary>
        /// Checks if given feature is enabled for current tenant.
        /// </summary>
        /// <param name="featureName">Name of the feature</param>
        /// <returns></returns>
        protected virtual Task<bool> IsEnabledAsync(string featureName)
        {
            return FeatureChecker.IsEnabledAsync(featureName);
        }

        /// <summary>
        /// Checks if given feature is enabled for current tenant.
        /// </summary>
        /// <param name="featureName">Name of the feature</param>
        /// <returns></returns>
        protected virtual bool IsEnabled(string featureName)
        {
            return FeatureChecker.IsEnabled(featureName);
        }

        /// <summary>
        /// Json the specified data, contentType, contentEncoding and behavior.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="contentType">Content type.</param>
        /// <param name="contentEncoding">Content encoding.</param>
        /// <param name="behavior">Behavior.</param>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            if (_wrapResultAttribute != null && !_wrapResultAttribute.WrapOnSuccess)
            {
                return base.Json(data, contentType, contentEncoding, behavior);
            }

            if (data == null)
            {
                data = new AjaxResponse();
            }
            else if (!ReflectionHelper.IsAssignableToGenericType(data.GetType(), typeof(AjaxResponse<>)))
            {
                data = new AjaxResponse(data);
            }

            return new ApmJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        #region OnActionExecuting / OnActionExecuted

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetCurrentMethodInfoAndWrapResultAttribute(filterContext);
            base.OnActionExecuting(filterContext);
        }

        private void SetCurrentMethodInfoAndWrapResultAttribute(ActionExecutingContext filterContext)
        {
            //Prevent overriding for child actions
            if (_currentMethodInfo != null)
            {
                return;
            }

            _currentMethodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();
            _wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                    _currentMethodInfo,
                    ApmMvcConfiguration.DefaultWrapResultAttribute
                );
        }

        #endregion

        #region Exception handling

        protected override void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //If exception handled before, do nothing.
            //If this is child action, exception should be handled by main action.
            if (context.ExceptionHandled || context.IsChildAction)
            {
                base.OnException(context);
                return;
            }

            //Log exception
            if (_wrapResultAttribute == null || _wrapResultAttribute.LogError)
            {
                LogHelper.LogException(Logger, context.Exception);
            }

            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information.
            if (!context.HttpContext.IsCustomErrorEnabled)
            {
                base.OnException(context);
                return;
            }

            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
            // ignore it.
            if (new HttpException(null, context.Exception).GetHttpCode() != 500)
            {
                base.OnException(context);
                return;
            }

            //Check WrapResultAttribute
            if (_wrapResultAttribute == null || !_wrapResultAttribute.WrapOnError)
            {
                base.OnException(context);
                return;
            }

            //We handled the exception!
            context.ExceptionHandled = true;

            //Return an error response to the client.
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = GetStatusCodeForException(context);
            context.Result = MethodInfoHelper.IsJsonResult(_currentMethodInfo)
                ? GenerateJsonExceptionResult(context)
                : GenerateNonJsonExceptionResult(context);

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            context.HttpContext.Response.TrySkipIisCustomErrors = true;

            //Trigger an event, so we can register it.
            EventBus.Trigger(this, new ApmHandledExceptionData(context.Exception));
        }

        protected virtual int GetStatusCodeForException(ExceptionContext context)
        {
            
            if (context.Exception is ApmAuthorizationException)
            {
                return context.HttpContext.User.Identity.IsAuthenticated
                    ? 403
                    : 401;
            }

            return 500;
        }
        
        protected virtual ActionResult GenerateJsonExceptionResult(ExceptionContext context)
        {
            context.HttpContext.Items.Add("IgnoreJsonRequestBehaviorDenyGet", "true");
            return new ApmJsonResult(
                new AjaxResponse(
                    ErrorInfoBuilder.BuildForException(context.Exception),
                    context.Exception is ApmAuthorizationException
                    )
                );
        }

        protected virtual ActionResult GenerateNonJsonExceptionResult(ExceptionContext context)
        {
            return new ViewResult
            {
                ViewName = "Error",
                MasterName = string.Empty,
                ViewData = new ViewDataDictionary<ErrorViewModel>(new ErrorViewModel(ErrorInfoBuilder.BuildForException(context.Exception), context.Exception)),
                TempData = context.Controller.TempData
            };
        }

        #endregion
    }
}