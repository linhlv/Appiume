using System.Linq;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;
using Appiume.Apm.Localization;
using Appiume.Apm.Localization.Dictionaries;
using Castle.Core.Logging;
using Appiume.Apm.Tenancy.Localization;

namespace Appiume.Apm.Tenancy.Configuration
{
    internal class LanguageManagementConfig : ILanguageManagementConfig
    {
        public ILogger Logger { get; set; }

        private readonly IIocManager _iocManager;
        private readonly IApmStartupConfiguration _configuration;

        public LanguageManagementConfig(IIocManager iocManager, IApmStartupConfiguration configuration)
        {
            _iocManager = iocManager;
            _configuration = configuration;

            Logger = NullLogger.Instance;
        }

        public void EnableDbLocalization()
        {
            _iocManager.Register<ILanguageProvider, ApplicationLanguageProvider>(DependencyLifeStyle.Transient);

            var sources = _configuration
                .Localization
                .Sources
                .Where(s => s is IDictionaryBasedLocalizationSource)
                .Cast<IDictionaryBasedLocalizationSource>()
                .ToList();
            
            foreach (var source in sources)
            {
                _configuration.Localization.Sources.Remove(source);
                _configuration.Localization.Sources.Add(
                    new MultiTenantLocalizationSource(
                        source.Name,
                        new MultiTenantLocalizationDictionaryProvider(
                            source.DictionaryProvider,
                            _iocManager
                            )
                        )
                    );

                Logger.DebugFormat("Converted {0} ({1}) to MultiTenantLocalizationSource", source.Name, source.GetType());
            }
        }
    }
}