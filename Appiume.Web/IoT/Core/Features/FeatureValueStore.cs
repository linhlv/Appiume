using Appiume.Apm.Application.Features;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Runtime.Caching;
using Appiume.Apm.Tenancy.Application.Features;
using Appiume.Apm.Tenancy.MultiTenancy;
using Appiume.Web.IoT.Core.Authorization.Roles;
using Appiume.Web.IoT.Core.Users;
using Appiume.Web.IoT.Core.MultiTenancy;

namespace Appiume.Web.IoT.Core.Features
{
    public class FeatureValueStore : ApmFeatureValueStore<Tenant, Role, User>
    {
        public FeatureValueStore(
            ICacheManager cacheManager,
            IRepository<TenantFeatureSetting, long> tenantFeatureSettingRepository,
            IRepository<Tenant> tenantRepository,
            IRepository<EditionFeatureSetting, long> editionFeatureSettingRepository,
            IFeatureManager featureManager,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                  cacheManager,
                  tenantFeatureSettingRepository,
                  tenantRepository,
                  editionFeatureSettingRepository,
                  featureManager,
                  unitOfWorkManager)
        {
        }
    }
}
