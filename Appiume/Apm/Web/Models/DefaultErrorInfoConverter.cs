using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Extensions;
using Appiume.Apm.Runtime.Validation;
using Appiume.Apm.UI;
using Appiume.Apm.Web.Configuration;
using Appiume.Apm.Web.Localization;

namespace Appiume.Apm.Web.Models
{
    //TODO@Halil: I did not like constructing ErrorInfo this way. It works wlll but I think we should change it later...
    internal class DefaultErrorInfoConverter : IExceptionToErrorInfoConverter
    {
        private readonly IApmWebModuleConfiguration _configuration;

        public IExceptionToErrorInfoConverter Next { set; private get; }

        private bool SendAllExceptionsToClients
        {
            get
            {
                return _configuration.SendAllExceptionsToClients;
            }
        }

        public DefaultErrorInfoConverter(IApmWebModuleConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ErrorInfo Convert(Exception exception)
        {
            if (SendAllExceptionsToClients)
            {
                return CreateDetailedErrorInfoFromException(exception);
            }

            if (exception is AggregateException && exception.InnerException != null)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerException is UserFriendlyException || aggException.InnerException is ApmValidationException)
                {
                    exception = aggException.InnerException;
                }
            }

            if (exception is UserFriendlyException)
            {
                var userFriendlyException = exception as UserFriendlyException;
                return new ErrorInfo(userFriendlyException.Code, userFriendlyException.Message, userFriendlyException.Details);
            }

            if (exception is ApmValidationException)
            {
                return new ErrorInfo(ApmWebLocalizedMessages.ValidationError)
                       {
                           ValidationErrors = GetValidationErrorInfos(exception as ApmValidationException),
                           Details = GetValidationErrorNarrative(exception as ApmValidationException)
                       };
            }

            if (exception is Appiume.Apm.Authorization.ApmAuthorizationException)
            {
                var authorizationException = exception as Appiume.Apm.Authorization.ApmAuthorizationException;
                return new ErrorInfo(authorizationException.Message);
            }

            return new ErrorInfo(ApmWebLocalizedMessages.InternalServerError);
        }

        private static ErrorInfo CreateDetailedErrorInfoFromException(Exception exception)
        {
            var detailBuilder = new StringBuilder();

            AddExceptionToDetails(exception, detailBuilder);

            var errorInfo = new ErrorInfo(exception.Message, detailBuilder.ToString());

            if (exception is ApmValidationException)
            {
                errorInfo.ValidationErrors = GetValidationErrorInfos(exception as ApmValidationException);
            }

            return errorInfo;
        }

        private static void AddExceptionToDetails(Exception exception, StringBuilder detailBuilder)
        {
            //Exception Message
            detailBuilder.AppendLine(exception.GetType().Name + ": " + exception.Message);

            //Additional info for UserFriendlyException
            if (exception is UserFriendlyException)
            {
                var userFriendlyException = exception as UserFriendlyException;
                if (!string.IsNullOrEmpty(userFriendlyException.Details))
                {
                    detailBuilder.AppendLine(userFriendlyException.Details);
                }
            }

            //Additional info for ApmValidationException
            if (exception is ApmValidationException)
            {
                var validationException = exception as ApmValidationException;
                if (validationException.ValidationErrors.Count > 0)
                {
                    detailBuilder.AppendLine(GetValidationErrorNarrative(validationException));
                }
            }

            //Exception StackTrace
            if (!string.IsNullOrEmpty(exception.StackTrace))
            {
                detailBuilder.AppendLine("STACK TRACE: " + exception.StackTrace);
            }

            //Inner exception
            if (exception.InnerException != null)
            {
                AddExceptionToDetails(exception.InnerException, detailBuilder);
            }

            //Inner exceptions for AggregateException
            if (exception is AggregateException)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerExceptions.IsNullOrEmpty())
                {
                    return;
                }

                foreach (var innerException in aggException.InnerExceptions)
                {
                    AddExceptionToDetails(innerException, detailBuilder);
                }
            }
        }

        private static ValidationErrorInfo[] GetValidationErrorInfos(ApmValidationException validationException)
        {
            var validationErrorInfos = new List<ValidationErrorInfo>();

            foreach (var validationResult in validationException.ValidationErrors)
            {
                var validationError = new ValidationErrorInfo(validationResult.ErrorMessage);

                if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
                {
                    validationError.Members = validationResult.MemberNames.Select(m => m.ToCamelCase()).ToArray();
                }

                validationErrorInfos.Add(validationError);
            }

            return validationErrorInfos.ToArray();
        }

        private static string GetValidationErrorNarrative(ApmValidationException validationException)
        {
            var detailBuilder = new StringBuilder();
            detailBuilder.AppendLine(ApmWebLocalizedMessages.ValidationNarrativeTitle);
            
            foreach (var validationResult in validationException.ValidationErrors)
            {
                detailBuilder.AppendFormat(" - {0}", validationResult.ErrorMessage);
                detailBuilder.AppendLine();
            }

            return detailBuilder.ToString();
        }
    }
}