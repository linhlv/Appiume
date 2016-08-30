using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Runtime.Validation
{
    /// <summary>
    /// Can be added to a method to disable auto validation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property)]
    public class DisableValidationAttribute : Attribute
    {

    }
}