using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.FirebaseCloudMessaging.Models
{
    public class FirebaseCloudMessagingResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string MulticastId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Failure { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long CanonicalIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string > Results { get; set; }
    }
}
