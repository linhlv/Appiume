using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Localization;
using Appiume.Apm.Localization.Sources;

namespace Appiume.Apm.Web.Localization
{
    /// <summary>
    /// This class is used to simplify getting localized messages in this assembly.
    /// TODO: DELETE THIS CLASS (except SourceName)
    /// </summary>
    internal static class ApmWebLocalizedMessages
    {
        public const string SourceName = "ApmWeb";

        public static string InternalServerError { get { return L("InternalServerError"); } }

        public static string ValidationError { get { return L("ValidationError"); } }

        public static string ValidationNarrativeTitle { get { return L("ValidationNarrativeTitle"); } }

        private static readonly ILocalizationSource Source;

        static ApmWebLocalizedMessages()
        {
            Source = LocalizationHelper.GetSource(SourceName);
        }

        private static string L(string name)
        {
            try
            {
                return Source.GetString(name);
            }
            catch (Exception)
            {
                return name;
            }
        }
    }
}