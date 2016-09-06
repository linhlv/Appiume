using Appiume.Apm.Application.Features;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Tenancy.Application.Features;
using Appiume.Apm.Tenancy.MultiTenancy;
using Appiume.Web.Dewey.Core.Authorization.Roles;
using Appiume.Web.Dewey.Core.Editions;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.MultiTenancy
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