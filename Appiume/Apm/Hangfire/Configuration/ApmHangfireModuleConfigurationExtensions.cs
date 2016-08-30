using System;
using Appiume.Apm.Hangfire;
using Appiume.Apm.Hangfire.Configuration;
using Appiume.Apm.BackgroundJobs;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;

namespace Appiume.Apm.Hangfire.Configuration
{
    public static class ApmHangfireConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Apm Hangfire module.
        /// </summary>
        public static IApmHangfireConfiguration ApmHangfire(this IModuleConfigurations configurations)
        {
            return configurations.ApmConfiguration.Get<IApmHangfireConfiguration>();
        }

        /// <summary>
        /// Configures to use Hangfire for background job management.
        /// </summary>
        public static void UseHangfire(this IBackgroundJobConfiguration backgroundJobConfiguration, Action<IApmHangfireConfiguration> configureAction)
        {
            backgroundJobConfiguration.ApmConfiguration.IocManager.RegisterIfNot<IBackgroundJobManager, HangfireBackgroundJobManager>();
            configureAction(backgroundJobConfiguration.ApmConfiguration.Modules.ApmHangfire());
        }
    }
}