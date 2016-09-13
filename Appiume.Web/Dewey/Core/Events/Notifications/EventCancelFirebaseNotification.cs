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
                To = "APA91bFN6cbnhqApgiEftvAuLEkwLNmDUl434qpRnPR6dLRTPZxETA6xbyJZefU77fxr2YkXTSzmYDkIgjiMtdN3SYRbY4amKAPyQ6E1A6m35BcLurtaOiBBeOZdAFaIhm9SOZT2G2zpZee9a_h7eg2AXUyQHtadqw",
                Data = new Dictionary<string, string>
                {
                    {"date", DateTime.Now.ToString()}
                }
            });
        }
    }
}