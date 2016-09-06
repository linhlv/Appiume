using Appiume.Apm.IdentityFramework;
using Appiume.Apm.UI;
using Appiume.Apm.Web.Mvc.Controllers;
using Appiume.Web.Dewey.Core;
using Microsoft.AspNet.Identity;

namespace Appiume.Web.Dewey.WebMvc.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class EventCloudControllerBase : ApmController
    {
        protected EventCloudControllerBase()
        {
            LocalizationSourceName = EventCloudConsts.LocalizationSourceName;
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