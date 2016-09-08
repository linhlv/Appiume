using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Dewey.Core.Friendships
{
    public enum FriendshipStatus
    {
        WaitingApprovalFromFriend = 0,

        WaitingApprovalFromUser = 1,

        Accepted = 2
    }
}