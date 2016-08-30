using Appiume.Apm.Collections;

namespace Appiume.Apm.Tenancy.Configuration
{
    /// <summary>
    /// User management configuration.
    /// </summary>
    public interface IUserManagementConfig
    {
        ITypeList<object> ExternalAuthenticationSources { get; set; }
    }
}