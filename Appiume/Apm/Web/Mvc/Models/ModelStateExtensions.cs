using System.Collections.Generic;
using System.Web.Mvc;
using Appiume.Apm.Web.Localization;
using Appiume.Apm.Web.Models;

namespace Appiume.Apm.Web.Mvc.Models
{
    public static class ModelStateExtensions
    {
        public static AjaxResponse ToAjaxResponse(this ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                return new AjaxResponse();
            }

            var validationErrors = new List<ValidationErrorInfo>();

            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    validationErrors.Add(new ValidationErrorInfo(error.ErrorMessage, state.Key));
                }
            }

            var errorInfo = new ErrorInfo(ApmWebLocalizedMessages.ValidationError)
                            {
                                ValidationErrors = validationErrors.ToArray()
                            };

            return new AjaxResponse(errorInfo);
        }
    }
}
