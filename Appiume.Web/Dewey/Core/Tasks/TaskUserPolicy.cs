using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Dewey.Core.Friendships;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Tasks
{
    public class TaskUserPolicy : ITaskUserPolicy
    {
        private readonly IFriendshipDomainService _friendshipDomainService;

        public TaskUserPolicy(IFriendshipDomainService friendshipDomainService)
        {
            _friendshipDomainService = friendshipDomainService;
        }

        public bool CanSeeProfile(ApmUser<User> requesterUser, ApmUser<User> targetUser)
        {
            return requesterUser.Id == targetUser.Id || _friendshipDomainService.HasFriendship(requesterUser, targetUser);
        }
    }
}