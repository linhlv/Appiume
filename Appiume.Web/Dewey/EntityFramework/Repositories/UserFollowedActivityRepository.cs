using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Ef;
using Appiume.Web.Dewey.Core.Activities;

namespace Appiume.Web.Dewey.EntityFramework.Repositories
{
    public class UserFollowedActivityRepository : TaskCloudRepositoryBase<UserFollowedActivity, long>, IUserFollowedActivityRepository
    {
        public UserFollowedActivityRepository(IDbContextProvider<DeweyDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }

        public IList<UserFollowedActivity> Getactivities(long userId, bool? isActor, long beforeId, int maxResultCount)
        {
            return new UserFollowedActivity[0];

            //var queryBuilder = new StringBuilder();
            //queryBuilder.AppendLine("from " + typeof(UserFollowedActivity).FullName + " as ufa");
            //queryBuilder.AppendLine("inner join fetch ufa.Activity as act");
            //queryBuilder.AppendLine("left outer join fetch act.Task as task");
            //queryBuilder.AppendLine("left outer join fetch act.CreatorUser as cusr");
            //queryBuilder.AppendLine("left outer join fetch act.AssignedUser as ausr");
            //queryBuilder.AppendLine("where ufa.User.Id = :userId and ufa.id < :beforeId");

            //if (isActor.HasValue)
            //{
            //    queryBuilder.AppendLine("and ufa.IsActor = :isActor");
            //}

            //queryBuilder.AppendLine("order by ufa.Id desc");

            //var query = Session
            //    .CreateQuery(queryBuilder.ToString())
            //    .SetParameter("userId", userId)
            //    .SetParameter("beforeId", beforeId);

            //if (isActor.HasValue)
            //{
            //    query.SetParameter("isActor", isActor.Value);
            //}

            //return query
            //    .SetMaxResults(maxResultCount)
            //    .List<UserFollowedActivity>();
        }
    }
}