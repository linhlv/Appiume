﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Dewey.Core.Friendships;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Tasks
{
    public class TaskPolicy : ITaskPolicy
    {
        private readonly IFriendshipDomainService _friendshipDomainService;
        private readonly IFriendshipRepository _friendshipRepository;

        public TaskPolicy(IFriendshipDomainService friendshipDomainService, IFriendshipRepository friendshipRepository)
        {
            _friendshipDomainService = friendshipDomainService;
            _friendshipRepository = friendshipRepository;
        }

        public bool CanSeeTasksOfUser(ApmUser<User> requesterUser, ApmUser<User> userOfTasks)
        {
            return requesterUser.Id == userOfTasks.Id ||
                   _friendshipDomainService.HasFriendship(requesterUser, userOfTasks);
        }

        public bool CanAssignTask(ApmUser<User> assignerUser, ApmUser<User> userToAssign)
        {
            if (assignerUser.Id == userToAssign.Id) //TODO: Override == to be able to write just assignerUser == userToAssign
            {
                return true;
            }

            var friendship = _friendshipRepository.GetOrNull(assignerUser.Id, userToAssign.Id, true);
            if (friendship == null)
            {
                return false;
            }

            return friendship.CanAssignTask;
        }

        public bool CanUpdateTask(ApmUser<User> user, Task task)
        {
            return (task.CreatorUserId == user.Id) || (task.AssignedUser.Id == user.Id);
        }

        public bool CanDeleteTask(ApmUser<User> user, Task task)
        {
            return (task.CreatorUserId == user.Id) || (task.AssignedUser.Id == user.Id);
        }
    }
}