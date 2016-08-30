using System.Threading.Tasks;
using Appiume.Apm.Application.Services;
using Appiume.Web.IoT.Application.Roles.Dto;

namespace Appiume.Web.IoT.Application.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
