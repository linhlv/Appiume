using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.AutoMapper;
using Appiume.Web.Modules.EventCloud.Core.MultiTenancy;

namespace Appiume.Web.Modules.EventCloud.Application.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}