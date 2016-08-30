using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Tenancy.Application.Features;

namespace Appiume.Apm.Tenancy.MultiTenancy
{
    /// <summary>
    /// Feature setting for a Tenant (<see cref="ApmTenant{TUser}"/>).
    /// </summary>
    public class TenantFeatureSetting : FeatureSetting, IMustHaveTenant
    {
        /// <summary>
        /// Tenant's Id.
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantFeatureSetting"/> class.
        /// </summary>
        public TenantFeatureSetting()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantFeatureSetting"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="name">Feature name.</param>
        /// <param name="value">Feature value.</param>
        public TenantFeatureSetting(int tenantId, string name, string value)
            :base(name, value)
        {
            TenantId = tenantId;
        }
    }
}