﻿using Appiume.Apm.Collections;

namespace Appiume.Apm.Notifications
{
    internal class NotificationConfiguration : INotificationConfiguration
    {
        public ITypeList<NotificationProvider> Providers { get; private set; }

        public NotificationConfiguration()
        {
            Providers = new TypeList<NotificationProvider>();
        }
    }
}