
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Friendships.Dto
{
    public class UpdateLastVisitTimeInput : IInputDto
    {
        public long FriendUserId { get; set; }
    }
}