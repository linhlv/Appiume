﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Localization;
using Appiume.Apm.Modules;
using Appiume.Apm.Threading;

namespace Appiume.Apm.Web
{
    /// <summary>
    /// This class is used to simplify starting of Apm system using <see cref="ApmBootstrapper"/> class..
    /// Inherit from this class in global.asax instead of <see cref="HttpApplication"/> to be able to start Apm system.
    /// </summary>
    /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="ApmModule"/>.</typeparam>
    public abstract class ApmWebApplication<TStartupModule> : HttpApplication
        where TStartupModule : ApmModule
    {
        /// <summary>
        /// Gets a reference to the <see cref="ApmBootstrapper"/> instance.
        /// </summary>
        protected ApmBootstrapper ApmBootstrapper { get; }

        protected ApmWebApplication()
        {
            ApmBootstrapper = ApmBootstrapper.Create<TStartupModule>();
        }

        /// <summary>
        /// This method is called by ASP.NET system on web application's startup.
        /// </summary>
        protected virtual void Application_Start(object sender, EventArgs e)
        {
            ThreadCultureSanitizer.Sanitize();
            ApmBootstrapper.Initialize();
        }

        /// <summary>
        /// This method is called by ASP.NET system on web application shutdown.
        /// </summary>
        protected virtual void Application_End(object sender, EventArgs e)
        {
            ApmBootstrapper.Dispose();
        }

        /// <summary>
        /// This method is called by ASP.NET system when a session starts.
        /// </summary>
        protected virtual void Session_Start(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method is called by ASP.NET system when a session ends.
        /// </summary>
        protected virtual void Session_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method is called by ASP.NET system when a request starts.
        /// </summary>
        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            var langCookie = Request.Cookies["Apm.Localization.CultureName"];
            if (langCookie != null && GlobalizationHelper.IsValidCultureCode(langCookie.Value))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(langCookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(langCookie.Value);
            }
            else if (!Request.UserLanguages.IsNullOrEmpty())
            {
                var firstValidLanguage = Request
                    .UserLanguages
                    .FirstOrDefault(GlobalizationHelper.IsValidCultureCode);

                if (firstValidLanguage != null)
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(firstValidLanguage);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(firstValidLanguage);
                }
            }
        }

        /// <summary>
        /// This method is called by ASP.NET system when a request ends.
        /// </summary>
        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            
        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {

        }
    }
}
