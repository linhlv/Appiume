using System.Threading;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;
using Appiume.Apm.Runtime.Session;
using Microsoft.AspNet.Identity;

namespace Appiume.Apm.Tenancy.Runtime.Session
{
    /// <summary>
    /// Implements IApmSession to get session informations from ASP.NET Identity framework.
    /// </summary>
    public class IdentityFrameworkClaimsApmSession : ClaimsApmSession, ISingletonDependency
    {
        public override long? UserId
        {
            get
            {
                var userIdAsString = Thread.CurrentPrincipal.Identity.GetUserId();
                if (string.IsNullOrEmpty(userIdAsString))
                {
                    return null;
                }

                long userId;
                if (!long.TryParse(userIdAsString, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public IdentityFrameworkClaimsApmSession(IMultiTenancyConfig multiTenancy) 
            : base(multiTenancy)
        {
        }
    }
}