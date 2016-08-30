using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Appiume.Apm
{
    /// <summary>
    /// This exception is thrown if a problem on Apm initialization progress.
    /// </summary>
    [Serializable]
    public class ApmInitializationException : ApmException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ApmInitializationException()
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public ApmInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public ApmInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public ApmInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}