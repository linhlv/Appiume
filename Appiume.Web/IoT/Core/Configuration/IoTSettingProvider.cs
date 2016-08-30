using System.Collections.Generic;
using Appiume.Apm.Configuration;
using Appiume.Web.IoT.Core.Configuration;

namespace Appiume.Web.IoT.Core.Configuration
{
    public class IoTSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                    IoTSettingNames.MaxAllowedEventRegistrationCountInLast30DaysPerUser,
                    defaultValue: "10",
                    scopes: SettingScopes.Tenant),
            };
        }
    }
}
