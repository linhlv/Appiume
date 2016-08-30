using Appiume.Apm.Dependency;
using Appiume.Apm.Events.Bus.Entities;
using Appiume.Apm.Events.Bus.Handlers;
using Appiume.Apm.Runtime.Caching;
using Appiume.Apm.Tenancy.Runtime.Caching;

namespace Appiume.Apm.Tenancy.Authorization.Roles
{
    public class ApmRolePermissionCacheItemInvalidator :
        IEventHandler<EntityChangedEventData<RolePermissionSetting>>,
        IEventHandler<EntityDeletedEventData<ApmRoleBase>>,
        ITransientDependency
    {
        private readonly ICacheManager _cacheManager;

        public ApmRolePermissionCacheItemInvalidator(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void HandleEvent(EntityChangedEventData<RolePermissionSetting> eventData)
        {
            var cacheKey = eventData.Entity.RoleId + "@" + (eventData.Entity.TenantId ?? 0);
            _cacheManager.GetRolePermissionCache().Remove(cacheKey);
        }

        public void HandleEvent(EntityDeletedEventData<ApmRoleBase> eventData)
        {
            var cacheKey = eventData.Entity.Id + "@" + (eventData.Entity.TenantId ?? 0);
            _cacheManager.GetRolePermissionCache().Remove(cacheKey);
        }
    }
}