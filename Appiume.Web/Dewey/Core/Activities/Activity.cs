using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Domain.Entities.Auditing;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Dewey.Core.Users;
using Appiume.Web.Dewey.Core.Tasks;

namespace Appiume.Web.Dewey.Core.Activities
{
    public abstract class Activity : Entity<long>, IHasCreationTime
    {
        //public virtual ActivityType ActivityType { get; set; }

        [ForeignKey("AssignedUserId")]
        public virtual ApmUser<User> AssignedUser { get; set; }
        public virtual long AssignedUserId { get; set; }

        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }
        public virtual int TaskId { get; set; }


        public virtual DateTime CreationTime { get; set; }

        protected Activity()
        {
            CreationTime = DateTime.Now;
        }

        public abstract long?[] GetActors();

        public abstract long?[] GetRelatedUsers();
    }
}