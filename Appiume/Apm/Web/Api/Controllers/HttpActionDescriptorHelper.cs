using System.Linq;
using System.Web.Http.Controllers;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Web.Models;

namespace Appiume.Apm.Web.WebApi.Controllers
{
    internal static class HttpActionDescriptorHelper
    {
        public static WrapResultAttribute GetWrapResultAttributeOrNull(HttpActionDescriptor actionDescriptor)
        {
            if (actionDescriptor == null)
            {
                return null;
            }

            //Try to get for dynamic APIs (dynamic web api actions always define __ApmDynamicApiDontWrapResultAttribute)
            var wrapAttr = actionDescriptor.Properties.GetOrDefault("__ApmDynamicApiDontWrapResultAttribute") as WrapResultAttribute;
            if (wrapAttr != null)
            {
                return wrapAttr;
            }

            //Get for the action
            wrapAttr = actionDescriptor.GetCustomAttributes<WrapResultAttribute>().FirstOrDefault();
            if (wrapAttr != null)
            {
                return wrapAttr;
            }

            //Get for the controller
            wrapAttr = actionDescriptor.ControllerDescriptor.GetCustomAttributes<WrapResultAttribute>().FirstOrDefault();
            if (wrapAttr != null)
            {
                return wrapAttr;
            }

            //Not found
            return null;
        }
    }
}