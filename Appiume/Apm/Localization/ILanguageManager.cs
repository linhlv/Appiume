using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Localization
{
    public interface ILanguageManager
    {
        LanguageInfo CurrentLanguage { get; }

        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}
