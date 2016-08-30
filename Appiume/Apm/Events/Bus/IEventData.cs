using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Events.Bus
{
    /// <summary>
    /// Defines interface for all Event data classes.
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// The time when the event occured.
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// The object which triggers the event (optional).
        /// </summary>
        object EventSource { get; set; }
    }
}
