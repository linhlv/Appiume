using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Configuration;
using Appiume.Apm.Localization;

namespace Appiume.Apm.Notifications
{
    public class NotificationSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                    NotificationSettingNames.ReceiveNotifications,
                    "true",
                    L("ReceiveNotifications"),
                    scopes: SettingScopes.User,
                    isVisibleToClients: true)
            };
        }

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, ApmConsts.LocalizationSourceName);
        }
    }
}