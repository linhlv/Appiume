using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Appiume.Apm.Auditing;
using Appiume.Apm.Threading;

namespace Appiume.Apm.Authorization
{
    public static class AuthorizationHelperExtensions
    {
        public static async Task AuthorizeAsync(this IAuthorizationHelper authorizationHelper, IApmAuthorizeAttribute authorizeAttribute)
        {
            await authorizationHelper.AuthorizeAsync(new[] { authorizeAttribute });
        }

        public static void Authorize(this IAuthorizationHelper authorizationHelper, IEnumerable<IApmAuthorizeAttribute> authorizeAttributes)
        {
            AsyncHelper.RunSync(() => authorizationHelper.AuthorizeAsync(authorizeAttributes));
        }

        public static void Authorize(this IAuthorizationHelper authorizationHelper, IApmAuthorizeAttribute authorizeAttribute)
        {
            authorizationHelper.Authorize(new[] { authorizeAttribute });
        }

        public static void Authorize(this IAuthorizationHelper authorizationHelper, MethodInfo methodInfo)
        {
            AsyncHelper.RunSync(() => authorizationHelper.AuthorizeAsync(methodInfo));
        }
    }
}