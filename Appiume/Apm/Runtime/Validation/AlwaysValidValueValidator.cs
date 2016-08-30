using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Runtime.Validation
{
   [Validator("NULL")]
    [Serializable]
    public class AlwaysValidValueValidator : ValueValidatorBase
    {
        public override bool IsValid(object value)
        {
            return true;
        }
    }
}