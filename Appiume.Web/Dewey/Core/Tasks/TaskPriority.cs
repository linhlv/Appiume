using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Dewey.Core.Tasks
{
    public enum TaskPriority : byte
    {
        Low = 1,
        BelowNormal = 2,
        Normal = 3,
        AboveNormal = 4,
        High = 5
    }
}