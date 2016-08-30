using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Dependency;

namespace Appiume.Apm.Notifications
{
    /// <summary>
    /// This class should be implemented in order to define notifications.
    /// </summary>
    public abstract class NotificationProvider : ITransientDependency
    {
        /// <summary>
        /// Used to add/manipulate notification definitions.
        /// </summary>
        /// <param name="context">Context</param>
        public abstract void SetNotifications(INotificationDefinitionContext context);
    }
}