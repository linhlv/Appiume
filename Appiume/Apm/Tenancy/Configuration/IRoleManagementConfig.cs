using System.Collections.Generic;

namespace Appiume.Apm.Tenancy.Configuration
{
    public interface IRoleManagementConfig
    {
        List<StaticRoleDefinition> StaticRoles { get; }
    }
}