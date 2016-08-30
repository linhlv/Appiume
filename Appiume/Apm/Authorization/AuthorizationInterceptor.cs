using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.DynamicProxy;
using Appiume.Apm.Authorization;

namespace Appiume.Apm.Authorization
{
    /// <summary>
    /// This class is used to intercept methods to make authorization if the method defined <see cref="ApmAuthorizeAttribute"/>.
    /// </summary>
    public class AuthorizationInterceptor : IInterceptor
    {
        private readonly IAuthorizationHelper _authorizationHelper;

        public AuthorizationInterceptor(IAuthorizationHelper authorizationHelper)
        {
            _authorizationHelper = authorizationHelper;
        }

        public void Intercept(IInvocation invocation)
        {
            _authorizationHelper.Authorize(invocation.MethodInvocationTarget);
            invocation.Proceed();
        }
    }
}