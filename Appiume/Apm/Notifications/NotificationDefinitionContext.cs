using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Notifications
{
    internal class NotificationDefinitionContext : INotificationDefinitionContext
    {
        public INotificationDefinitionManager Manager { get; private set; }

        public NotificationDefinitionContext(INotificationDefinitionManager manager)
        {
            Manager = manager;
        }
    }
}