using System.Threading.Tasks;
using Appiume.Apm.Auditing;
using Appiume.Apm.Authorization;
using Appiume.Apm.AutoMapper;
using Appiume.Web.IoT.Application.Sessions.Dto;

namespace Appiume.Web.IoT.Application.Sessions
{
    [ApmAuthorize]
    public class SessionAppService : IoTAppServiceBase, ISessionAppService
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