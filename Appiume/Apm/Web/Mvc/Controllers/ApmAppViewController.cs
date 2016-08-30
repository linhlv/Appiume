using System.Web.Mvc;
using Appiume.Apm.Auditing;

namespace Appiume.Apm.Web.Mvc.Controllers
{
    //TODO: Maybe it's better to write an HTTP handler for that instead of controller (since it's more light)
    public class ApmAppViewController : ApmController
    {
        [DisableAuditing]
        public ActionResult Load(string viewUrl)
        {
            if (!viewUrl.StartsWith("~"))
            {
                viewUrl = "~" + viewUrl;
            }

            return View(viewUrl);
        }
    }
}
