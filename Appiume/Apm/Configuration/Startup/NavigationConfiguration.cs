using Appiume.Apm.Application.Navigation;
using Appiume.Apm.Collections;

namespace Appiume.Apm.Configuration.Startup
{
    internal class NavigationConfiguration : INavigationConfiguration
    {
        public ITypeList<NavigationProvider> Providers { get; private set; }

        public NavigationConfiguration()
        {
            Providers = new TypeList<NavigationProvider>();
        }
    }
}