using System;

namespace Appiume.Apm.Tenancy.Ldap.Configuration
{
    public interface IApmTenancyLdapModuleConfig
    {
        bool IsEnabled { get; }

        Type AuthenticationSourceType { get; }

        void Enable(Type authenticationSourceType);
    }
}