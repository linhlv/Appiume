using System.Collections.Generic;

namespace Appiume.Apm.Localization
{
    public interface ILanguageProvider
    {
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}