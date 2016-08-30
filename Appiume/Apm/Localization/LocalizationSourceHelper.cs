using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Extensions;
using Appiume.Apm.Logging;

namespace Appiume.Apm.Localization
{
    public static class LocalizationSourceHelper
    {
        public static string ReturnGivenNameOrThrowException(ILocalizationConfiguration configuration, string sourceName, string name, CultureInfo culture)
        {
            var exceptionMessage = string.Format(
                "Can not find '{0}' in localization source '{1}'!",
                name, sourceName
                );

            if (!configuration.ReturnGivenTextIfNotFound)
            {
                throw new ApmException(exceptionMessage);
            }

            LogHelper.Logger.Warn(exceptionMessage);

            var notFoundText = configuration.HumanizeTextIfNotFound
                ? name.ToSentenceCase(culture)
                : name;

            return configuration.WrapGivenTextIfNotFound
                ? string.Format("[{0}]", notFoundText)
                : notFoundText;
        }
    }
}