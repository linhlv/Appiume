using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Appiume.Apm.Auditing;
using Appiume.Apm.Extensions;
using Appiume.Apm.Timing;
using Appiume.Apm.Web.Authorization;
using Appiume.Apm.Web.Features;
using Appiume.Apm.Web.Localization;
using Appiume.Apm.Web.MultiTenancy;
using Appiume.Apm.Web.Navigation;
using Appiume.Apm.Web.Sessions;
using Appiume.Apm.Web.Settings;
using Appiume.Apm.Web.Timing;

namespace Appiume.Apm.Web.Mvc.Controllers
{
    /// <summary>
    /// This controller is used to create client side scripts
    /// to work with Appiume.Apm.
    /// </summary>
    public class ApmScriptsController : ApmController
    {
        private readonly IMultiTenancyScriptManager _multiTenancyScriptManager;
        private readonly ISettingScriptManager _settingScriptManager;
        private readonly INavigationScriptManager _navigationScriptManager;
        private readonly ILocalizationScriptManager _localizationScriptManager;
        private readonly IAuthorizationScriptManager _authorizationScriptManager;
        private readonly IFeaturesScriptManager _featuresScriptManager;
        private readonly ISessionScriptManager _sessionScriptManager;
        private readonly ITimingScriptManager _timingScriptManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ApmScriptsController(
            IMultiTenancyScriptManager multiTenancyScriptManager,
            ISettingScriptManager settingScriptManager,
            INavigationScriptManager navigationScriptManager,
            ILocalizationScriptManager localizationScriptManager,
            IAuthorizationScriptManager authorizationScriptManager,
            IFeaturesScriptManager featuresScriptManager,
            ISessionScriptManager sessionScriptManager, 
            ITimingScriptManager timingScriptManager)
        {
            _multiTenancyScriptManager = multiTenancyScriptManager;
            _settingScriptManager = settingScriptManager;
            _navigationScriptManager = navigationScriptManager;
            _localizationScriptManager = localizationScriptManager;
            _authorizationScriptManager = authorizationScriptManager;
            _featuresScriptManager = featuresScriptManager;
            _sessionScriptManager = sessionScriptManager;
            _timingScriptManager = timingScriptManager;
        }

        /// <summary>
        /// Gets all needed scripts.
        /// </summary>
        [DisableAuditing]
        public async Task<ActionResult> GetScripts(string culture = "")
        {
            if (!culture.IsNullOrEmpty())
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }

            var sb = new StringBuilder();

            sb.AppendLine(_multiTenancyScriptManager.GetScript());
            sb.AppendLine();

            sb.AppendLine(_sessionScriptManager.GetScript());
            sb.AppendLine();

            sb.AppendLine(_localizationScriptManager.GetScript());
            sb.AppendLine();

            sb.AppendLine(await _featuresScriptManager.GetScriptAsync());
            sb.AppendLine();

            sb.AppendLine(await _authorizationScriptManager.GetScriptAsync());
            sb.AppendLine();

            sb.AppendLine(await _navigationScriptManager.GetScriptAsync());
            sb.AppendLine();

            sb.AppendLine(await _settingScriptManager.GetScriptAsync());
            sb.AppendLine();

            sb.AppendLine(await _timingScriptManager.GetScriptAsync());
            sb.AppendLine();

            sb.AppendLine(GetTriggerScript());

            

            return Content(sb.ToString(), "application/x-javascript", Encoding.UTF8);
        }

        private static string GetTriggerScript()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(){");
            script.AppendLine("    Appiume.Apm.event.trigger('Appiume.Apm.dynamicScriptsInitialized');");
            script.Append("})();");

            return script.ToString();
        }
    }
}
