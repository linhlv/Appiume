using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.FirebaseCloudMessaging.Models;

namespace Appiume.Apm.FirebaseCloudMessaging
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFirebaseCloudMessagingNotification
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firebaseCloudMessagingRequest"></param>
        /// <returns></returns>
        FirebaseCloudMessagingResponse Push(FirebaseCloudMessagingRequest firebaseCloudMessagingRequest);
    }
}
