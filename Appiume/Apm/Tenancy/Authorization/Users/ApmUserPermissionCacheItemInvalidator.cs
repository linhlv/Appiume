using Appiume.Apm.Dependency;
using Appiume.Apm.Events.Bus.Entities;
using Appiume.Apm.Events.Bus.Handlers;
using Appiume.Apm.Runtime.Caching;
using Appiume.Apm.Tenancy.Runtime.Caching;

namespace Appiume.Apm.Tenancy.Authorization.Users
{
    public class ApmUserPermissionCacheItemInvalidator :
        IEventHandler<EntityChangedEventData<UserPermissionSetting>>,
        IEventHandler<EntityChangedEventData<UserRole>>,
        IEventHandler<EntityDeletedEventData<ApmUserBase>>,

        ITransientDependency
    {
        private readonly ICacheManager _cacheManager;

        public ApmUserPermissionCacheItemInvalidator(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void HandleEvent(EntityChangedEventData<UserPermissionSetting> eventData)
        {
            var cacheKey = eventData.Entity.UserId + "@" + (eventData.Entity.TenantId ?? 0);
            _cacheManager.GetUserPermissionCache().Remove(cacheKey);
        }

        public void HandleEvent(EntityChangedEventData<UserRole> eventData)
        {
            var cacheKey = eventData.Entity.UserId + "@" + (eventData.Entity.TenantId ?? 0);
            _cacheManager.GetUserPermissionCache().Remove(cacheKey);
        }

        public void HandleEvent(EntityDeletedEventData<ApmUserBase> eventData)
        {
            var cacheKey = eventData.Entity.Id + "@" + (eventData.Entity.TenantId ?? 0);
            _cacheManager.GetUserPermissionCache().Remove(cacheKey);
        }
    }
}