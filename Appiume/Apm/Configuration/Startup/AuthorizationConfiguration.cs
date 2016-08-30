using Appiume.Apm.Authorization;
using Appiume.Apm.Collections;

namespace Appiume.Apm.Configuration.Startup
{
    internal class AuthorizationConfiguration : IAuthorizationConfiguration
    {
        public ITypeList<AuthorizationProvider> Providers { get; private set; }

        public AuthorizationConfiguration()
        {
            Providers = new TypeList<AuthorizationProvider>();
        }
    }
}