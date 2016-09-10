using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Dependency;
using Appiume.Apm.Events.Bus.Handlers;

namespace Appiume.Web.Dewey.Core.Events.Notifications
{
    public class EventCancelFirebaseNotification : IEventHandler<EventCancelledEvent>, ITransientDependency
    {
        public void HandleEvent(EventCancelledEvent eventData)
        {
           
        }
    }
}