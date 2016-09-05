﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Modules.TaskCloud.Core.Tasks
{
    /// <summary>
    /// Possible states of a <see cref="Task"/>.
    /// </summary>
    public enum TaskState : byte
    {
        /// <summary>
        /// The task is active.
        /// </summary>
        Active = 1,

        /// <summary>
        /// The task is completed.
        /// </summary>
        Completed = 2
    }
}