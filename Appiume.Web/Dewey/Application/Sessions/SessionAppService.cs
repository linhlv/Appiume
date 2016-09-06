using System.Threading.Tasks;
using Appiume.Apm.Auditing;
using Appiume.Apm.Authorization;
using Appiume.Apm.AutoMapper;
using Appiume.Web.Dewey.Application.Sessions.Dto;

namespace Appiume.Web.Dewey.Application.Sessions
{
    [ApmAuthorize]
    public class SessionAppService : EventCloudAppServiceBase, ISessionAppService
    {
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                User = (await GetCurrentUserAsync()).MapTo<UserLoginInfoDto>()
            };

            if (ApmSession.TenantId.HasValue)
            {
                output.Tenant = (await GetCurrentTenantAsync()).MapTo<TenantLoginInfoDto>();
            }

            return output;
        }
    }
}