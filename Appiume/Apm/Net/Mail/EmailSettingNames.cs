using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Net.Mail
{
    /// <summary>
    /// Declares names of the settings defined by <see cref="EmailSettingProvider"/>.
    /// </summary>
    public static class EmailSettingNames
    {
        /// <summary>
        /// Apm.Net.Mail.DefaultFromAddress
        /// </summary>
        public const string DefaultFromAddress = "Apm.Net.Mail.DefaultFromAddress";

        /// <summary>
        /// Apm.Net.Mail.DefaultFromDisplayName
        /// </summary>
        public const string DefaultFromDisplayName = "Apm.Net.Mail.DefaultFromDisplayName";

        /// <summary>
        /// SMTP related email settings.
        /// </summary>
        public static class Smtp
        {
            /// <summary>
            /// Apm.Net.Mail.Smtp.Host
            /// </summary>
            public const string Host = "Apm.Net.Mail.Smtp.Host";

            /// <summary>
            /// Apm.Net.Mail.Smtp.Port
            /// </summary>
            public const string Port = "Apm.Net.Mail.Smtp.Port";

            /// <summary>
            /// Apm.Net.Mail.Smtp.UserName
            /// </summary>
            public const string UserName = "Apm.Net.Mail.Smtp.UserName";

            /// <summary>
            /// Apm.Net.Mail.Smtp.Password
            /// </summary>
            public const string Password = "Apm.Net.Mail.Smtp.Password";

            /// <summary>
            /// Apm.Net.Mail.Smtp.Domain
            /// </summary>
            public const string Domain = "Apm.Net.Mail.Smtp.Domain";

            /// <summary>
            /// Apm.Net.Mail.Smtp.EnableSsl
            /// </summary>
            public const string EnableSsl = "Apm.Net.Mail.Smtp.EnableSsl";

            /// <summary>
            /// Apm.Net.Mail.Smtp.UseDefaultCredentials
            /// </summary>
            public const string UseDefaultCredentials = "Apm.Net.Mail.Smtp.UseDefaultCredentials";
        }
    }
}