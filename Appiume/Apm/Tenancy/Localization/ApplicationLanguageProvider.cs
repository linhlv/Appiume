using System.Collections.Generic;
using System.Linq;
using Appiume.Apm.Localization;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.Tenancy.Runtime.Session;
using Appiume.Apm.Threading;

namespace Appiume.Apm.Tenancy.Localization
{
    /// <summary>
    /// Implements <see cref="ILanguageProvider"/> to get languages from <see cref="IApplicationLanguageManager"/>.
    /// </summary>
    public class ApplicationLanguageProvider : ILanguageProvider
    {
        /// <summary>
        /// Reference to the session.
        /// </summary>
        public IApmSession ApmSession { get; set; }

        private readonly IApplicationLanguageManager _applicationLanguageManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ApplicationLanguageProvider(IApplicationLanguageManager applicationLanguageManager)
        {
            _applicationLanguageManager = applicationLanguageManager;

            ApmSession = NullApmSession.Instance;
        }

        /// <summary>
        /// Gets the languages for current tenant.
        /// </summary>
        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            var languageInfos = AsyncHelper.RunSync(() => _applicationLanguageManager.GetLanguagesAsync(ApmSession.TenantId))
                    .OrderBy(l => l.DisplayName)
                    .Select(l => l.ToLanguageInfo())
                    .ToList();

            SetDefaultLanguage(languageInfos);

            return languageInfos;
        }

        private void SetDefaultLanguage(List<LanguageInfo> languageInfos)
        {
            if (languageInfos.Count <= 0)
            {
                return;
            }

            var defaultLanguage = AsyncHelper.RunSync(() => _applicationLanguageManager.GetDefaultLanguageOrNullAsync(ApmSession.TenantId));
            if (defaultLanguage == null)
            {
                languageInfos[0].IsDefault = true;
                return;
            }
            
            var languageInfo = languageInfos.FirstOrDefault(l => l.Name == defaultLanguage.Name);
            if (languageInfo == null)
            {
                languageInfos[0].IsDefault = true;
                return;
            }

            languageInfo.IsDefault = true;
        }
    }
}