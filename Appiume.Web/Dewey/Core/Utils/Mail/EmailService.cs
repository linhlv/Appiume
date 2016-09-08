using System;
using System.Net.Mail;
using Appiume.Apm.Configuration;
using Castle.Core.Logging;

namespace Appiume.Web.Dewey.Core.Utils.Mail
{
    //TODO: Get setting from configuration
    /// <summary>
    /// Implements <see cref="IEmailService"/> to send emails using current settings.
    /// </summary>
    public class EmailService : IEmailService
    {
        public ILogger Logger { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="EmailService"/>.
        /// </summary>
        public EmailService()
        {
            Logger = NullLogger.Instance;
        }

        public void SendEmail(MailMessage mail)
        {
            try
            {
                mail.From = new MailAddress(SettingHelper.GetSettingValue("Apm.Net.Mail.SenderAddress"), SettingHelper.GetSettingValue("Apm.Net.Mail.DisplayName"));
                using (var client = new SmtpClient(SettingHelper.GetSettingValue("Apm.Net.Mail.ServerAddress"), SettingHelper.GetSettingValue<int>("Apm.Net.Mail.ServerPort")))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(SettingHelper.GetSettingValue("Apm.Net.Mail.Username"), SettingHelper.GetSettingValue("Apm.Net.Mail.Password"));
                    client.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Logger.Warn("Could not send email!", ex);
            }
        }
    }
}