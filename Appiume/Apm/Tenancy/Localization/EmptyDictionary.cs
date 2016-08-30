using System.Collections.Generic;
using System.Globalization;
using Appiume.Apm.Localization;
using Appiume.Apm.Localization.Dictionaries;

namespace Appiume.Apm.Tenancy.Localization
{
    internal class EmptyDictionary : ILocalizationDictionary
    {
        public CultureInfo CultureInfo { get; private set; }

        public EmptyDictionary(CultureInfo cultureInfo)
        {
            CultureInfo = cultureInfo;
        }

        public LocalizedString GetOrNull(string name)
        {
            return null;
        }

        public IReadOnlyList<LocalizedString> GetAllStrings()
        {
            return new LocalizedString[0];
        }

        public string this[string name]
        {
            get { return null; }
            set { }
        }
    }
}