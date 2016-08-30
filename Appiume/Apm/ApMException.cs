using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Appiume.Apm
{
    /// <summary>
    /// Base exception type for those are thrown by Apm system for Apm specific exceptions.
    /// </summary>
    [Serializable]
    public class ApmException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="ApmException"/> object.
        /// </summary>
        public ApmException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ApmException"/> object.
        /// </summary>
        public ApmException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="ApmException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public ApmException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="ApmException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public ApmException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}