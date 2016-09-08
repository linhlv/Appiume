using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Friendships.Dto
{
    public class RemoveFriendshipInput :IInputDto
    {
        public int Id { get; set; } //TODO: Get UserId and FriendId instead of Id?
    }
}