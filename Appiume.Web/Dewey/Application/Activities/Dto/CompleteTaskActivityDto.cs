using Appiume.Web.Dewey.Application.Tasks.Dtos;
using Appiume.Web.Dewey.Application.Users.Dto;

namespace Appiume.Web.Dewey.Application.Activities.Dto
{
    public class CompleteTaskActivityDto : ActivityDto
    {
        public UserDto AssignedUser { get; set; }

        public TaskDto Task { get; set; }
    }
}