using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.AutoMapper;
using Appiume.Web.Modules.EventCloud.Core.Users;

namespace Appiume.Web.Modules.EventCloud.Application.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }
    }
}
