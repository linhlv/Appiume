using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Domain.Repositories;

namespace Appiume.Web.Dewey.Core.Activities
{
    public interface IUserFollowedActivityRepository : IRepository<UserFollowedActivity, long>
    {
        IList<UserFollowedActivity> Getactivities(long userId, bool? isActor, long beforeId, int maxResultCount);
    }
}
