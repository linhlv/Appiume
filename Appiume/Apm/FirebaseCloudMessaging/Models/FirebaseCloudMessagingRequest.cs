using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.FirebaseCloudMessaging.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class FirebaseCloudMessagingRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string AuthorizationKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Data { get; set; }
    }
}
