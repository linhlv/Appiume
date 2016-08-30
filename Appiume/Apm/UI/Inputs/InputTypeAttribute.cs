using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.UI.Inputs
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InputTypeAttribute : Attribute
    {
        public string Name { get; set; }

        public InputTypeAttribute(string name)
        {
            Name = name;
        }
    }
}