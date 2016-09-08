using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Users.Dto
{
    public class GetUserOutput : IOutputDto
    {
        public UserDto User { get; set; }

        public GetUserOutput(UserDto user)
        {
            User = user;
        }
    }
}