using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Dewey.Core.Activities
{
    public class CompleteTaskActivity : Activity
    {
        public CompleteTaskActivity()
        {
            //ActivityType = ActivityType.CompleteTask;            
        }

        public override long?[] GetActors()
        {
            return new[] { (long?)AssignedUser.Id };
        }

        public override long?[] GetRelatedUsers()
        {
            return new[] { Task.CreatorUserId };
        }
    }
}