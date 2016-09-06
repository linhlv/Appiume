using System;
using System.Linq;
using System.Threading.Tasks;
using Appiume.Apm.Authorization;
using Appiume.Web.Dewey.Application.Roles.Dto;
using Appiume.Web.Dewey.Core.Authorization.Roles;

namespace Appiume.Web.Dewey.Application.Roles
{
    /* THIS IS JUST A SAMPLE. */
    public class RoleAppService : EventCloudAppServiceBase, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IPermissionManager _permissionManager;

        public RoleAppService(RoleManager roleManager, IPermissionManager permissionManager)
        {
            _roleManager = roleManager;
            _permissionManager = permissionManager;
        }

        public async Task UpdateRolePermissions(UpdateRolePermissionsInput input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.RoleId);
            var grantedPermissions = _permissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissionNames.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }
    }
}