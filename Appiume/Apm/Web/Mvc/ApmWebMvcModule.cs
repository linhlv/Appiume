using System;
using System.Reflection;
using System.Web.Mvc;
using Appiume.Apm.Modules;
using Appiume.Apm.Web.Mvc.Auditing;
using Appiume.Apm.Web.Mvc.Authorization;
using Appiume.Apm.Web.Mvc.Configuration;
using Appiume.Apm.Web.Mvc.Controllers;
using Appiume.Apm.Web.Mvc.ModelBinding.Binders;
using Appiume.Apm.Web.Mvc.Uow;
using Appiume.Apm.Web.Mvc.Validation;

namespace Appiume.Apm.Web.Mvc
{
    /// <summary>
    /// This module is used to build ASP.NET MVC web sites using Appiume.Apm.
    /// </summary>
    [DependsOn(typeof(ApmWebModule))]
    public class ApmWebMvcModule : ApmModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.Register<IApmMvcConfiguration, ApmMvcConfiguration>();

            IocManager.AddConventionalRegistrar(new ControllerConventionalRegistrar());
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IocManager));
        }

        /// <inheritdoc/>
        public override void PostInitialize()
        {
            GlobalFilters.Filters.Add(IocManager.Resolve<ApmMvcAuthorizeFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<ApmMvcAuditFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<ApmMvcValidationFilter>());
            GlobalFilters.Filters.Add(IocManager.Resolve<ApmMvcUowFilter>());

            var apmMvcDateTimeBinder = new ApmMvcDateTimeBinder();
            ModelBinders.Binders.Add(typeof(DateTime), apmMvcDateTimeBinder);
            ModelBinders.Binders.Add(typeof(DateTime?), apmMvcDateTimeBinder);
        }
    }
}
