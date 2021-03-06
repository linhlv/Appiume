using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Friendships.Dto
{
    public class ChangeFriendshipPropertiesInput : IInputDto
    {
        public int Id { get; set; }

        public bool? FollowActivities { get; set; }

        public bool? CanAssignTask { get; set; }
    }
}