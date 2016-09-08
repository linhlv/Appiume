using System.Net.Mail;
using Appiume.Apm.Domain.Services;

namespace Appiume.Web.Dewey.Core.Utils.Mail
{
    /// <summary>
    /// This service is used to send emails.
    /// </summary>
    public interface IEmailService : IDomainService
    {
        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="mail">Email to be sent</param>
        void SendEmail(MailMessage mail);
    }
}