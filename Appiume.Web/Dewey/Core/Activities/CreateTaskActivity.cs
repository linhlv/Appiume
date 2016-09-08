using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Activities
{
    public class CreateTaskActivity : Activity
    {
        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }

        public virtual long CreatorUserId { get; set; }

        public CreateTaskActivity()
        {
            //ActivityType = ActivityType.CreateTask;
        }

        public override long?[] GetActors()
        {
            return new long?[] { CreatorUser.Id, AssignedUser.Id };
        }

        public override long?[] GetRelatedUsers()
        {
            return new long?[] { };
        }
    }
}