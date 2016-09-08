﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Appiume.Apm.Ef;
using Appiume.Web.Dewey.Core.Activities;
using Appiume.Web.Dewey.Core.Friendships;

namespace Appiume.Web.Dewey.EntityFramework.Repositories
{
    public class FriendshipRepository : TaskCloudRepositoryBase<Friendship>, IFriendshipRepository
    {
        public FriendshipRepository(IDbContextProvider<DeweyDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }

        public List<Friendship> GetAllWithFriendUser(long userId, FriendshipStatus? status, bool? canAssignTask)
        {
            var query = GetAll()
                .Include(f => f.Friend)
                .Where(f => f.User.Id == userId);

            if (status.HasValue)
            {
                query = query.Where(friendship => friendship.Status == status.Value);
            }

            if (canAssignTask.HasValue)
            {
                query = query.Where(friendship => friendship.CanAssignTask == canAssignTask);
            }

            query = query.OrderBy(f => f.Friend.Name).ThenBy(f => f.Friend.Surname);

            return query.ToList();
        }

        public IQueryable<Friendship> GetAllWithFriendUser(long userId)
        {
            return GetAll()
                .Include(f => f.Friend)
                .Where(f => f.User.Id == userId);
        }

        public Friendship GetOrNull(long userId, long friendId, bool onlyAccepted = false)
        {
            var query = from friendship in GetAll()
                        where friendship.User.Id == userId && friendship.Friend.Id == friendId
                        select friendship;

            if (onlyAccepted)
            {
                query = query.Where(f => f.Status == FriendshipStatus.Accepted);
            }

            return query.FirstOrDefault();
        }
    }
}