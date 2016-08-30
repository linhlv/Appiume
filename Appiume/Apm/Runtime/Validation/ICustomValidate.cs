using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Runtime.Validation
{
    /// <summary>
    /// Defines interface that must be implemented by classes those must be validated with custom rules.
    /// So, implementing class can define it's own validation logic.
    /// </summary>
    public interface ICustomValidate
    {
        /// <summary>
        /// This method is used to validate the object.
        /// </summary>
        /// <param name="results">List of validation results (errors). Add validation errors to this list.</param>
        void AddValidationErrors(List<ValidationResult> results);
    }
}
