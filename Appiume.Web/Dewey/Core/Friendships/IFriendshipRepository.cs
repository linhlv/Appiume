using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Domain.Repositories;

namespace Appiume.Web.Dewey.Core.Friendships
{
    public interface IFriendshipRepository : IRepository<Friendship>
    {
        List<Friendship> GetAllWithFriendUser(long userId, FriendshipStatus? status, bool? canAssignTask);

        IQueryable<Friendship> GetAllWithFriendUser(long userId);

        Friendship GetOrNull(long userId, long friendId, bool onlyAccepted = false);
    }
}
