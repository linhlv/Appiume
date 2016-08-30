using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Runtime.Validation
{
     [AttributeUsage(AttributeTargets.Class)]
    public class ValidatorAttribute : Attribute
    {
        public string Name { get; set; }

        public ValidatorAttribute(string name)
        {
            Name = name;
        }
    }
}