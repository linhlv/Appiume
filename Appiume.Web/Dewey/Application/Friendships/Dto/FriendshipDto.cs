using System;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Web.Dewey.Application.Users.Dto;
using Appiume.Web.Dewey.Core.Friendships;

namespace Appiume.Web.Dewey.Application.Friendships.Dto
{
    public class FriendshipDto 
    {
        public UserDto Friend { get; set; }

        public bool FollowActivities { get; set; }

        public bool CanAssignTask { get; set; }

        public FriendshipStatus Status { get; set; }

        public DateTime CreationTime { get; set; }
    }
}