using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Dewey.Core.Tasks
{
    public enum TaskPrivacy : byte
    {
        /// <summary>
        /// Only the creator user can see the task
        /// </summary>
        Private = 1,

        /// <summary>
        /// Only friends can see the task.
        /// </summary>
        Protected = 2
    }
}