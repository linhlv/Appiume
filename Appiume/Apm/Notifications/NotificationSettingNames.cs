using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Configuration;
using Appiume.Apm.Localization;

namespace Appiume.Apm.Notifications
{
    /// <summary>
    /// Pre-defined setting names for notification system.
    /// </summary>
    public static class NotificationSettingNames
    {
        /// <summary>
        /// A top-level switch to enable/disable receiving notifications for a user.
        /// "Apm.Notifications.ReceiveNotifications".
        /// </summary>
        public const string ReceiveNotifications = "Apm.Notifications.ReceiveNotifications";
    }
}