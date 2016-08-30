using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Configuration.Startup;

namespace Appiume.Apm.BackgroundJobs
{
    /// <summary>
    /// Used to configure background job system.
    /// </summary>
    public interface IBackgroundJobConfiguration
    {
        /// <summary>
        /// Used to enable/disable background job execution.
        /// </summary>
        bool IsJobExecutionEnabled { get; set; }

        /// <summary>
        /// Gets the APM configuration object.
        /// </summary>
        IApmStartupConfiguration ApmConfiguration { get; }
    }
}
