using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Users.Dto
{
    public class GetCurrentUserInfoOutput : IOutputDto
    {
        public UserDto User { get; set; }
    }
}