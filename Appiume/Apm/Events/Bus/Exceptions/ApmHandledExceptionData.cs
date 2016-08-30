using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Events.Bus.Exceptions
{
    /// <summary>
    /// This type of events are used to notify for exceptions handled by Apm infrastructure.
    /// </summary>
    public class ApmHandledExceptionData : ExceptionData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception object</param>
        public ApmHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}