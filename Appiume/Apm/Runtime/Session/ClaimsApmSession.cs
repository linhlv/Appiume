using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Runtime.Security;

namespace Appiume.Apm.Runtime.Session
{
    /// <summary>
    /// Implements <see cref="IApmSession"/> to get session properties from claims of <see cref="Thread.CurrentPrincipal"/>.
    /// </summary>
    public class ClaimsApmSession : IApmSession
    {
        protected virtual ClaimsPrincipal Principal => Thread.CurrentPrincipal as ClaimsPrincipal;
        protected virtual ClaimsIdentity Identity => Principal?.Identity as ClaimsIdentity;

        public virtual long? UserId
        {
            get
            {
                var userIdClaim = Identity?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }

                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        public virtual int? TenantId
        {
            get
            {
                if (!MultiTenancy.IsEnabled)
                {
                    return MultiTenancyConsts.DefaultTenantId;
                }

                var tenantIdClaim = Principal?.Claims.FirstOrDefault(c => c.Type == ApmClaimTypes.TenantId);
                if (string.IsNullOrEmpty(tenantIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt32(tenantIdClaim.Value);
            }
        }

        public virtual long? ImpersonatorUserId
        {
            get
            {
                var impersonatorUserIdClaim = Principal?.Claims.FirstOrDefault(c => c.Type == ApmClaimTypes.ImpersonatorUserId);
                if (string.IsNullOrEmpty(impersonatorUserIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt64(impersonatorUserIdClaim.Value);
            }
        }

        public virtual int? ImpersonatorTenantId
        {
            get
            {
                if (!MultiTenancy.IsEnabled)
                {
                    return MultiTenancyConsts.DefaultTenantId;
                }

                var impersonatorTenantIdClaim = Principal?.Claims.FirstOrDefault(c => c.Type == ApmClaimTypes.ImpersonatorTenantId);
                if (string.IsNullOrEmpty(impersonatorTenantIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt32(impersonatorTenantIdClaim.Value);
            }
        }

        public virtual MultiTenancySides MultiTenancySide
        {
            get
            {
                return MultiTenancy.IsEnabled && !TenantId.HasValue
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant;
            }
        }

        protected readonly IMultiTenancyConfig MultiTenancy;

        public ClaimsApmSession(IMultiTenancyConfig multiTenancy)
        {
            MultiTenancy = multiTenancy;
        }
    }
}