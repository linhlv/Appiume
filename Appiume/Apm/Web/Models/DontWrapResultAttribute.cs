﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Web.Models
{
    /// <summary>
    /// A shortcut for <see cref="WrapResultAttribute"/> to disable wrapping by default.
    /// It sets false to <see cref="WrapResultAttribute.WrapOnSuccess"/> and <see cref="WrapResultAttribute.WrapOnError"/>  properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class DontWrapResultAttribute : WrapResultAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DontWrapResultAttribute"/> class.
        /// </summary>
        public DontWrapResultAttribute()
            : base(false, false)
        {

        }
    }
}