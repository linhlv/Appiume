using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Web.Dewey.Core.Utils.Mail;

namespace Appiume.Web.Dewey.Core.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;

        public NotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void Notify(INotification notification)
        {
            var mail = notification.CreateMailMessage();
            _emailService.SendEmail(mail);
        }
    }
}