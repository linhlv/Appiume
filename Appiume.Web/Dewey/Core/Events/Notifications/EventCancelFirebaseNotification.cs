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

            _firebaseCloudMessagingNotification.Push(new FirebaseCloudMessagingRequest
            {
                AuthorizationKey = "AIzaSyAQaoUDHhdaBPeMx1hEEeDh9_DsX794CR8",
                To = "faHQTaDcVGs:APA91bGc8R7Vm0x4tyNpjoCnuJEaZ7Upb2pR6uQf9ErfMt8eoG6v8HqnNs5Y9wj5DAXfwsq2IPO2utEIES8G0n7SexeXI4eaSV-Bg9geIEBC70E2RClFWt7lrbZMcdsr0TYy0wYNxz3M",
                Data = new Dictionary<string, string>
                {
                    {"date", DateTime.Now.ToString()}
                }
            });
        }
    }
}