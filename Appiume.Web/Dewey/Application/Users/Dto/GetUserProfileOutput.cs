using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Users.Dto
{
    public class GetUserProfileOutput :IOutputDto
    {
        public bool CanNotSeeTheProfile { get; set; }

        public bool SentFriendshipRequest { get; set; }

        public UserDto User { get; set; }
    }
}