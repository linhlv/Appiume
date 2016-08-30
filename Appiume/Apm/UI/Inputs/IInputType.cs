using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Runtime.Validation;

namespace Appiume.Apm.UI.Inputs
{
    public interface IInputType
    {
        string Name { get; }

        object this[string key] { get; set; }

        IDictionary<string, object> Attributes { get; }

        IValueValidator Validator { get; set; }
    }
}
