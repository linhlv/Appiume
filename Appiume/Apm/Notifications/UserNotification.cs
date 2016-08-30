using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Apm.Notifications
{
    /// <summary>
    /// Represents a notification sent to a user.
    /// </summary>
    public class UserNotification : EntityDto<Guid>, IUserIdentifier
    {
        /// <summary>
        /// TenantId.
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// User Id.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Current state of the user notification.
        /// </summary>
        public UserNotificationState State { get; set; }

        /// <summary>
        /// The notification.
        /// </summary>
        public TenantNotification TenantNotification { get; set; }
    }
}