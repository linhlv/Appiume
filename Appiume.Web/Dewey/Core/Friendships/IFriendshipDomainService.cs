using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Domain.Services;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Friendships
{
    public interface IFriendshipDomainService : IDomainService
    {
        bool HasFriendship(ApmUser<User> user, ApmUser<User> probableFriend);
    }
}
