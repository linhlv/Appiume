using System.Web.Mvc;
using Appiume.Apm.Web.Mvc.Authorization;
using Appiume.Web.IoT.Controllers;

namespace Appiume.Web.Controllers
{
    [ApmMvcAuthorize]
    public class HomeController : IoTControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}