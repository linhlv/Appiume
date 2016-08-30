using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Collections;

namespace Appiume.Apm.Notifications
{
    /// <summary>
    /// Used to configure notification system.
    /// </summary>
    public interface INotificationConfiguration
    {
        /// <summary>
        /// Notification providers.
        /// </summary>
        ITypeList<NotificationProvider> Providers { get; }
    }
}
