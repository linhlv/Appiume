using System;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Web.Dewey.Application.Users.Dto;

namespace Appiume.Web.Dewey.Application.Activities.Dto
{
    public class UserFollowedActivityDto : EntityDto<long>
    {
        public UserDto User { get; set; }

        public ActivityDto Activity { get; set; }

        public bool IsActor { get; set; }

        public bool IsRelated { get; set; }

        public DateTime CreationTime { get; set; }
    }
}