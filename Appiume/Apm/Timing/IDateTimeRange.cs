using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Timing
{
    /// <summary>
    /// Defines interface for a DateTime range.
    /// </summary>
    public interface IDateTimeRange
    {
        /// <summary>
        /// Start time of the datetime range.
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// End time of the datetime range.
        /// </summary>
        DateTime EndTime { get; set; }
    }
}
