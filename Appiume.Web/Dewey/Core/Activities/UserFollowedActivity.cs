using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Domain.Entities.Auditing;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Activities
{
    public class UserFollowedActivity : Entity<long>, IHasCreationTime
    {
        public virtual ApmUser<User> User { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual bool IsActor { get; set; }

        public virtual bool IsRelated { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public UserFollowedActivity()
        {
            CreationTime = DateTime.Now;
        }
    }
}