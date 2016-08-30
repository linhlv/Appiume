using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Configuration;
using Appiume.Apm.Localization;

namespace Appiume.Apm.Timing
{
    public class TimingSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(TimingSettingNames.TimeZone, "UTC", L("TimeZone"), scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, isVisibleToClients: true)
            };
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, ApmConsts.LocalizationSourceName);
        }
    }
}