using System.Collections.Generic;
using System.Collections.Immutable;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;

namespace Appiume.Apm.Localization
{
    public class DefaultLanguageProvider : ILanguageProvider, ITransientDependency
    {
        private readonly ILocalizationConfiguration _configuration;

        public DefaultLanguageProvider(ILocalizationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _configuration.Languages.ToImmutableList();
        }
    }
}