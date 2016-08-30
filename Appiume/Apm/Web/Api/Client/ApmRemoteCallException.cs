using System;
using System.Runtime.Serialization;
using Appiume.Apm.Web.Models;

namespace Appiume.Apm.Web.WebApi.Client
{
    /// <summary>
    /// This exception is thrown when a remote method call made and remote application sent an error message.
    /// </summary>
    [Serializable]
    public class ApmRemoteCallException : ApmException
    {
        /// <summary>
        /// Remote error information.
        /// </summary>
        public ErrorInfo ErrorInfo { get; set; }

        /// <summary>
        /// Creates a new <see cref="ApmException"/> object.
        /// </summary>
        public ApmRemoteCallException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ApmException"/> object.
        /// </summary>
        public ApmRemoteCallException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="ApmException"/> object.
        /// </summary>
        /// <param name="errorInfo">Exception message</param>
        public ApmRemoteCallException(ErrorInfo errorInfo)
            : base(errorInfo.Message)
        {
            ErrorInfo = errorInfo;
        }
    }
}
