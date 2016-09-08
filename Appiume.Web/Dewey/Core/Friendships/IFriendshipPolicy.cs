using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Domain.Policies;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Friendships
{
    public interface IFriendshipPolicy : IPolicy
    {
        bool CanChangeFriendshipProperties(ApmUser<User> currentUser, Friendship friendShip);
        bool CanRemoveFriendship(ApmUser<User> currentUser, Friendship friendship);
    }
}
