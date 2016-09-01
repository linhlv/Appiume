using Appiume.Apm.Application.Features;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Tenancy.Application.Features;
using Appiume.Apm.Tenancy.MultiTenancy;
using Appiume.Web.Modules.EventCloud.Core.Authorization.Roles;
using Appiume.Web.Modules.EventCloud.Core.Editions;
using Appiume.Web.Modules.EventCloud.Core.Users;

namespace Appiume.Web.Modules.EventCloud.Core.MultiTenancy
{
    public class TenantManager : ApmTenantManager<Tenant, Role, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IApmTenancyFeatureValueStore featureValueStore
            ) : base(
            tenantRepository, 
            tenantFeatureRepository,
            editionManager,
            featureValueStore)
        {
        }
    }
}