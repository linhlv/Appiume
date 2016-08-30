using Appiume.Apm.IdentityFramework;
using Appiume.Apm.UI;
using Appiume.Apm.Web.Mvc.Controllers;
using Appiume.Web.IoT.Core;
using Microsoft.AspNet.Identity;

namespace Appiume.Web.IoT.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class IoTControllerBase : ApmController
    {
        protected IoTControllerBase()
        {
            LocalizationSourceName = IoTConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}