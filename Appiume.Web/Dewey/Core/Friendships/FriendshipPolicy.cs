using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Friendships
{
    public class FriendshipPolicy : IFriendshipPolicy
    {
        public bool CanChangeFriendshipProperties(ApmUser<User> user, Friendship friendShip)
        {
            //Can change only his own friendships.
            return user.Id == friendShip.User.Id;
        }

        public bool CanRemoveFriendship(ApmUser<User> currentUser, Friendship friendship)
        {
            return friendship.User.Id == currentUser.Id;
        }
    }
}