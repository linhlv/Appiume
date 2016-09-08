using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Appiume.Web.Dewey.Core.Notifications
{
    public interface INotification
    {
        MailMessage CreateMailMessage();
    }
}
