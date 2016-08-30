using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Net.Mail
{
    /// <summary>
    /// Defines configurations used while sending emails.
    /// </summary>
    public interface IEmailSenderConfiguration
    {
        /// <summary>
        /// Default from address.
        /// </summary>
        string DefaultFromAddress { get; }

        /// <summary>
        /// Default display name.
        /// </summary>
        string DefaultFromDisplayName { get; }
    }
}
