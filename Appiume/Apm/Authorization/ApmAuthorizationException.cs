using System;
using System.Runtime.Serialization;
using Appiume.Apm.Logging;

namespace Appiume.Apm.Authorization
{
    /// <summary>
    /// This exception is thrown on an unauthorized request.
    /// </summary>
    [Serializable]
    public class ApmAuthorizationException : ApmException, IHasLogSeverity
    {
        /// <summary>
        /// Severity of the exception.
        /// Default: Warn.
        /// </summary>
        public LogSeverity Severity { get; set; }

        /// <summary>
        /// Creates a new <see cref="ApmAuthorizationException"/> object.
        /// </summary>
        public ApmAuthorizationException()
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Creates a new <see cref="ApmAuthorizationException"/> object.
        /// </summary>
        public ApmAuthorizationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="ApmAuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public ApmAuthorizationException(string message)
            : base(message)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Creates a new <see cref="ApmAuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public ApmAuthorizationException(string message, Exception innerException)
            : base(message, innerException)
        {
            Severity = LogSeverity.Warn;
        }
    }
}