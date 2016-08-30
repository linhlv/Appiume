using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.BackgroundJobs
{
    /// <summary>
    /// Defines interface of a background job.
    /// </summary>
    public interface IBackgroundJob<in TArgs>
    {
        /// <summary>
        /// Executes the job with the <see cref="args"/>.
        /// </summary>
        /// <param name="args">Job arguments.</param>
        void Execute(TArgs args);
    }
}
