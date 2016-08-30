using Appiume.Apm.Application.Features;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Tenancy.Application.Features;
using Appiume.Apm.Tenancy.MultiTenancy;
using Appiume.Web.IoT.Core.Authorization.Roles;
using Appiume.Web.IoT.Core.Editions;
using Appiume.Web.IoT.Core.Users;

namespace Appiume.Web.IoT.Core.MultiTenancy
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