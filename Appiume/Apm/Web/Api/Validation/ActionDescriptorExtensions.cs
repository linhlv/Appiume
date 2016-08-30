using System.Reflection;
using System.Web.Http.Controllers;
using Appiume.Apm.Extensions;

namespace Appiume.Apm.Web.WebApi.Validation
{
    public static class ActionDescriptorExtensions
    {
        public static MethodInfo GetMethodInfoOrNull(this HttpActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ReflectedHttpActionDescriptor)
            {
                return actionDescriptor.As<ReflectedHttpActionDescriptor>().MethodInfo;
            }
            
            return null;
        }

        public static bool IsDynamicApmAction(this HttpActionDescriptor actionDescriptor)
        {
            return actionDescriptor
                .ControllerDescriptor
                .Properties
                .ContainsKey("__ApmDynamicApiControllerInfo");
        }
    }
}