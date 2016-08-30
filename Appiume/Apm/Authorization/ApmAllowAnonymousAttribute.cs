using System;

namespace Appiume.Apm.Authorization
{
    /// <summary>
    /// Used to allow a method to be accessed by any user.
    /// Suppress <see cref="ApmAuthorizeAttribute"/> defined in the class containing that method.
    /// </summary>
    public class ApmAllowAnonymousAttribute : Attribute, IApmAllowAnonymousAttribute
    {

    }
}