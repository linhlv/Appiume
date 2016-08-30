using Appiume.Apm.Configuration.Startup;

namespace Appiume.Apm.BackgroundJobs
{
    internal class BackgroundJobConfiguration : IBackgroundJobConfiguration
    {
        public bool IsJobExecutionEnabled { get; set; }
        
        public IApmStartupConfiguration ApmConfiguration { get; private set; }

        public BackgroundJobConfiguration(IApmStartupConfiguration apmConfiguration)
        {
            ApmConfiguration = apmConfiguration;

            IsJobExecutionEnabled = true;
        }
    }
}