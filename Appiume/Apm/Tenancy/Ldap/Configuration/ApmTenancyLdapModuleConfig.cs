using System;
using Appiume.Apm.Tenancy.Configuration;

namespace Appiume.Apm.Tenancy.Ldap.Configuration
{
    public class ApmTenancyLdapModuleConfig : IApmTenancyLdapModuleConfig
    {
        public bool IsEnabled { get; private set; }

        public Type AuthenticationSourceType { get; private set; }

        private readonly IApmTenancyConfig _tenancyConfig;

        public ApmTenancyLdapModuleConfig(IApmTenancyConfig tenancyConfig)
        {
            _tenancyConfig = tenancyConfig;
        }

        public void Enable(Type authenticationSourceType)
        {
            AuthenticationSourceType = authenticationSourceType;
            IsEnabled = true;

            _tenancyConfig.UserManagement.ExternalAuthenticationSources.Add(authenticationSourceType);
        }
    }
}