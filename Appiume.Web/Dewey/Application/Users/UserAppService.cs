using System.Threading.Tasks;
using Appiume.Apm.Authorization;
using Appiume.Web.Dewey.Application.Users.Dto;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Application.Users
{
    /* THIS IS JUST A SAMPLE. */
    public class UserAppService : EventCloudAppServiceBase, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly IPermissionManager _permissionManager;

        public UserAppService(UserManager userManager, IPermissionManager permissionManager)
        {
            _userManager = userManager;
            _permissionManager = permissionManager;
        }

        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await _userManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await _userManager.RemoveFromRoleAsync(userId, roleName));
        }
    }
}