using System.Threading.Tasks;
using Appiume.Apm.Application.Services;
using Appiume.Web.Dewey.Application.Roles.Dto;

namespace Appiume.Web.Dewey.Application.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
