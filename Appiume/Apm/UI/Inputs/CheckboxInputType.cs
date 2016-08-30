using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Runtime.Validation;

namespace Appiume.Apm.UI.Inputs
{
    [Serializable]
    [InputType("CHECKBOX")]
    public class CheckboxInputType : InputTypeBase
    {
        public CheckboxInputType()
            : this(new BooleanValueValidator())
        {

        }

        public CheckboxInputType(IValueValidator validator)
            : base(validator)
        {

        }
    }
}