using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Events.Bus.Entities;

namespace Appiume.Web.Dewey.Core.Tasks.Events
{
    public class TaskCompletedEventData : EntityEventData<Task>
    {
        public TaskCompletedEventData(Task entity)
            : base(entity)
        {
        }
    }
}