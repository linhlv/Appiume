using System.Threading.Tasks;
using Appiume.Apm.Application.Services;
using Appiume.Web.Modules.EventCloud.Application.Roles.Dto;

namespace Appiume.Web.Modules.EventCloud.Application.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
