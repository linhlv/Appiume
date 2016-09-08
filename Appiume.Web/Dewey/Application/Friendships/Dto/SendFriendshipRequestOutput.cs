
using Appiume.Apm.Application.Services.Dto;
using Appiume.Web.Dewey.Core.Friendships;

namespace Appiume.Web.Dewey.Application.Friendships.Dto
{
    public class SendFriendshipRequestOutput : IOutputDto
    {
        public FriendshipStatus Status { get; set; }
    }
}