using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Timing;

namespace Appiume.Apm.Events.Bus
{
    /// <summary>
    /// Implements <see cref="IEventData"/> and provides a base for event data classes.
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        /// <summary>
        /// The time when the event occured.
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// The object which triggers the event (optional).
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected EventData()
        {
            EventTime = Clock.Now;
        }
    }
}