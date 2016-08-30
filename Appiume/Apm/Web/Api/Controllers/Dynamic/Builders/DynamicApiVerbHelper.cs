using System;
using Appiume.Apm.Web;

namespace Appiume.Apm.Web.WebApi.Controllers.Dynamic.Builders
{
    /// <summary>
    /// NOTE: This is not used (as all members are private)
    /// </summary>
    internal static class DynamicApiVerbHelper
    {
        public  static HttpVerb GetConventionalVerbForMethodName(string methodName)
        {
            if (methodName.StartsWith("Get", StringComparison.InvariantCultureIgnoreCase))
            {
                return HttpVerb.Get;
            }

            if (methodName.StartsWith("Put", StringComparison.InvariantCultureIgnoreCase) || 
                methodName.StartsWith("Update", StringComparison.InvariantCultureIgnoreCase))
            {
                return HttpVerb.Put;
            }

            if (methodName.StartsWith("Delete", StringComparison.InvariantCultureIgnoreCase) ||
                methodName.StartsWith("Remove", StringComparison.InvariantCultureIgnoreCase))
            {
                return HttpVerb.Delete;
            }

            if (methodName.StartsWith("Post", StringComparison.InvariantCultureIgnoreCase) || 
                methodName.StartsWith("Create", StringComparison.InvariantCultureIgnoreCase) ||
                methodName.StartsWith("Insert", StringComparison.InvariantCultureIgnoreCase))
            {
                return HttpVerb.Post;
            }

            return GetDefaultHttpVerb();
        }

        public static HttpVerb GetDefaultHttpVerb()
        {
            return HttpVerb.Post;
        }
    }
}