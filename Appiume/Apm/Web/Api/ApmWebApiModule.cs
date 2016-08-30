using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Appiume.Apm.Logging;
using Appiume.Apm.Modules;
using Appiume.Apm.Web;
using Appiume.Apm.Web.WebApi.Configuration;
using Appiume.Apm.Web.WebApi.Controllers;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic.Formatters;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic.Selectors;
using Appiume.Apm.Web.WebApi.Runtime.Caching;
using Castle.MicroKernel.Registration;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Description;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Json;
using Appiume.Apm.Web.WebApi.Auditing;
using Appiume.Apm.Web.WebApi.Authorization;
using Appiume.Apm.Web.WebApi.Configuration.Startup;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic.Binders;
using Appiume.Apm.Web.WebApi.ExceptionHandling;
using Appiume.Apm.Web.WebApi.Uow;
using Appiume.Apm.Web.WebApi.Validation;

namespace Appiume.Apm.Web.WebApi
{
    /// <summary>
    /// This module provides Apm features for ASP.NET Web API.
    /// </summary>
    [DependsOn(typeof(ApmWebModule))]
    public class ApmWebApiModule : ApmModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());
            IocManager.Register<IApmWebApiConfiguration, ApmWebApiConfiguration>();

            Configuration.Settings.Providers.Add<ClearCacheSettingProvider>();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        
        public override void PostInitialize()
        {
            var httpConfiguration = IocManager.Resolve<IApmWebApiConfiguration>().HttpConfiguration;

            InitializeAspNetServices(httpConfiguration);
            InitializeFilters(httpConfiguration);
            InitializeFormatters(httpConfiguration);
            InitializeRoutes(httpConfiguration);
            InitializeModelBinders(httpConfiguration);

            foreach (var controllerInfo in DynamicApiControllerManager.GetAll())
            {
                IocManager.IocContainer.Register(
                    Component.For(controllerInfo.InterceptorType).LifestyleTransient(),
                    Component.For(controllerInfo.ApiControllerType)
                        .Proxy.AdditionalInterfaces(controllerInfo.ServiceInterfaceType)
                        .Interceptors(controllerInfo.InterceptorType)
                        .LifestyleTransient()
                    );

                LogHelper.Logger.DebugFormat("Dynamic web api controller is created for type '{0}' with service name '{1}'.", controllerInfo.ServiceInterfaceType.FullName, controllerInfo.ServiceName);
            }

            Configuration.Modules.ApmWebApi().HttpConfiguration.EnsureInitialized();
        }

        private void InitializeAspNetServices(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Services.Replace(typeof(IHttpControllerSelector), new ApmHttpControllerSelector(httpConfiguration));
            httpConfiguration.Services.Replace(typeof(IHttpActionSelector), new ApmApiControllerActionSelector(IocManager.Resolve<IApmWebApiConfiguration>()));
            httpConfiguration.Services.Replace(typeof(IHttpControllerActivator), new ApmApiControllerActivator(IocManager));
            httpConfiguration.Services.Replace(typeof(IApiExplorer), new ApmApiExplorer(IocManager.Resolve<IApmWebApiConfiguration>(), httpConfiguration));
        }

        private void InitializeFilters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Filters.Add(IocManager.Resolve<ApmApiAuthorizeFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<ApmApiAuditFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<ApmApiValidationFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<ApmApiUowFilter>());
            httpConfiguration.Filters.Add(IocManager.Resolve<ApmApiExceptionFilterAttribute>());

            httpConfiguration.MessageHandlers.Add(IocManager.Resolve<ResultWrapperHandler>());
        }

        private static void InitializeFormatters(HttpConfiguration httpConfiguration)
        {
            //Remove formatters except JsonFormatter.
            foreach (var currentFormatter in httpConfiguration.Formatters.ToList())
            {
                if (!(currentFormatter is JsonMediaTypeFormatter))
                {
                    httpConfiguration.Formatters.Remove(currentFormatter);
                }
            }

            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.Converters.Insert(0, new ApmDateTimeConverter());
            httpConfiguration.Formatters.Add(new PlainTextFormatter());
        }

        private static void InitializeRoutes(HttpConfiguration httpConfiguration)
        {
            //Dynamic Web APIs

            httpConfiguration.Routes.MapHttpRoute(
                name: "ApmDynamicWebApi",
                routeTemplate: "api/services/{*serviceNameWithAction}"
                );

            //Other routes

            httpConfiguration.Routes.MapHttpRoute(
                name: "ApmCacheController_Clear",
                routeTemplate: "api/ApmCache/Clear",
                defaults: new { controller = "ApmCache", action = "Clear" }
                );

            httpConfiguration.Routes.MapHttpRoute(
                name: "ApmCacheController_ClearAll",
                routeTemplate: "api/ApmCache/ClearAll",
                defaults: new { controller = "ApmCache", action = "ClearAll" }
                );
        }

        private static void InitializeModelBinders(HttpConfiguration httpConfiguration)
        {
            var apmApiDateTimeBinder = new ApmApiDateTimeBinder();
            httpConfiguration.BindParameter(typeof(DateTime), apmApiDateTimeBinder);
            httpConfiguration.BindParameter(typeof(DateTime?), apmApiDateTimeBinder);
        }
    }
}
