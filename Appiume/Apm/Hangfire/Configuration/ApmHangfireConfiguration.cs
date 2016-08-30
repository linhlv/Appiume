using Hangfire;
using HangfireGlobalConfiguration = Hangfire.GlobalConfiguration;

namespace Appiume.Apm.Hangfire.Configuration
{
    public class ApmHangfireConfiguration : IApmHangfireConfiguration
    {
        public BackgroundJobServer Server { get; set; }

        public IGlobalConfiguration GlobalConfiguration
        {
            get { return HangfireGlobalConfiguration.Configuration; }
        }
    }
}