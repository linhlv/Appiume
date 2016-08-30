using System.Web;
using System.Web.Mvc;
using Appiume.Apm.Auditing;
using Appiume.Apm.Configuration;
using Appiume.Apm.Localization;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.Timing;
using Appiume.Apm.Web.Models;

namespace Appiume.Apm.Web.Mvc.Controllers.Localization
{
    public class ApmLocalizationController : ApmController
    {
        [DisableAuditing]
        public virtual ActionResult ChangeCulture(string cultureName, string returnUrl = "")
        {
            if (!GlobalizationHelper.IsValidCultureCode(cultureName))
            {
                throw new ApmException("Unknown language: " + cultureName + ". It must be a valid culture!");
            }

            Response.Cookies.Add(new HttpCookie("Appiume.Apm.Localization.CultureName", cultureName) { Expires = Clock.Now.AddYears(2) });
            SettingManager.ChangeSettingForUser(ApmSession.ToUserIdentifier(), LocalizationSettingNames.DefaultLanguage, cultureName);

            if (Request.IsAjaxRequest())
            {
                return Json(new AjaxResponse(), JsonRequestBehavior.AllowGet);
            }

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect(Request.ApplicationPath);
        }
    }
}
