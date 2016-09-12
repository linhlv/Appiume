using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Dependency;
using Appiume.Apm.Events.Bus.Handlers;
using Appiume.Apm.FirebaseCloudMessaging;
using Appiume.Apm.FirebaseCloudMessaging.Models;

namespace Appiume.Web.Dewey.Core.Events.Notifications
{
    /// <summary>
    ///
    /// </summary>
    public class EventCancelFirebaseNotification : IEventHandler<EventCancelledEvent>, ITransientDependency
    {
        /// <summary>
        ///
        /// </summary>
        private readonly IFirebaseCloudMessagingNotification _firebaseCloudMessagingNotification;

        /// <summary>
        ///
        /// </summary>
        /// <param name="firebaseCloudMessagingNotification"></param>
        public EventCancelFirebaseNotification(IFirebaseCloudMessagingNotification firebaseCloudMessagingNotification)
        {
            _firebaseCloudMessagingNotification = firebaseCloudMessagingNotification;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(EventCancelledEvent eventData)
        {
            _firebaseCloudMessagingNotification.Push(new FirebaseCloudMessagingRequest
            {
                AuthorizationKey = "AIzaSyAQaoUDHhdaBPeMx1hEEeDh9_DsX794CR8",
                To = "APA91bHmlxBi6Km5uASYxIdj3-sYbwFxZkYrCKu19BgIq3DWYgBTIWf_YQrCdWkJq-8sALIpvfliQEqtG51o9m9QUvdZaZsB229U38JLp4JFkLKNCSzS8YG9QwycSdRZ935pu8AYWxxfVCDhYkVrG_45mDD9fP--VQ",
                Data = new Dictionary<string, string>
                {
                    {"date", DateTime.Now.ToString()}
                }
            });
        }
    }
}