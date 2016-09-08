using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Friendships
{
    public class FriendshipDomainService : IFriendshipDomainService
    {
        private readonly IFriendshipRepository _friendshipRepository;

        public FriendshipDomainService(IFriendshipRepository friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
        }

        public bool HasFriendship(ApmUser<User> user, ApmUser<User> probableFriend)
        {
            return _friendshipRepository.Query( //TODO: Create Index: UserId, FriendId, Status
               q => q.Any(friendship =>
                          friendship.User.Id == user.Id &&
                          friendship.Friend.Id == probableFriend.Id &&
                          friendship.Status == FriendshipStatus.Accepted
                        ));
        }
    }
}