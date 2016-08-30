namespace Appiume.Apm.Configuration.Startup
{
    internal class ModuleConfigurations : IModuleConfigurations
    {
        public IApmStartupConfiguration ApmConfiguration { get; private set; }

        public ModuleConfigurations(IApmStartupConfiguration apmConfiguration)
        {
            ApmConfiguration = apmConfiguration;
        }
    }
}