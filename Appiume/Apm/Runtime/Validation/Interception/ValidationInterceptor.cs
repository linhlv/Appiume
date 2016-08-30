using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Aspects;
using Appiume.Apm.Dependency;
using Castle.DynamicProxy;

namespace Appiume.Apm.Runtime.Validation.Interception
{
    /// <summary>
    /// This interceptor is used intercept method calls for classes which's methods must be validated.
    /// </summary>
    public class ValidationInterceptor : IInterceptor
    {
        private readonly IIocResolver _iocResolver;

        public ValidationInterceptor(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public void Intercept(IInvocation invocation)
        {
            if (ApmCrossCuttingConcerns.IsApplied(invocation.InvocationTarget, ApmCrossCuttingConcerns.Validation))
            {
                invocation.Proceed();
                return;
            }

            using (var validator = _iocResolver.ResolveAsDisposable<MethodInvocationValidator>())
            {
                validator.Object.Initialize(invocation.Method, invocation.Arguments);
                validator.Object.Validate();
            }

            invocation.Proceed();
        }
    }
}