using Appiume.Web.Dewey.Application.Tasks.Dtos;
using Appiume.Web.Dewey.Application.Users.Dto;

namespace Appiume.Web.Dewey.Application.Activities.Dto
{
    public class CreateTaskActivityDto : ActivityDto
    {
        public UserDto CreatorUser { get; set; }

        public UserDto AssignedUser { get; set; }

        public TaskDto Task { get; set; }
    }
}