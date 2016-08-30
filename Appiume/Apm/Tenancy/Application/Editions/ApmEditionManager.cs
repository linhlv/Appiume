using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appiume.Apm.Application.Features;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Domain.Services;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Events.Bus.Entities;
using Appiume.Apm.Events.Bus.Handlers;
using Appiume.Apm.Runtime.Caching;
using Appiume.Apm.Tenancy.Application.Features;
using Appiume.Apm.Tenancy.Runtime.Caching;

namespace Appiume.Apm.Tenancy.Application.Editions
{
    public abstract class ApmEditionManager : 
        IDomainService, 
        IEventHandler<EntityChangedEventData<Edition>>,
        IEventHandler<EntityChangedEventData<EditionFeatureSetting>>
    {
        private readonly IApmTenancyFeatureValueStore _featureValueStore;
        public IQueryable<Edition> Editions { get { return EditionRepository.GetAll(); } }

        public ICacheManager CacheManager { get; set; }

        public IFeatureManager FeatureManager { get; set; }

        protected IRepository<Edition> EditionRepository { get; set; }

        protected ApmEditionManager(
            IRepository<Edition> editionRepository,
            IApmTenancyFeatureValueStore featureValueStore)
        {
            _featureValueStore = featureValueStore;
            EditionRepository = editionRepository;
        }

        public virtual Task<string> GetFeatureValueOrNullAsync(int editionId, string featureName)
        {
            return _featureValueStore.GetEditionValueOrNullAsync(editionId, featureName);
        }

        public virtual Task SetFeatureValueAsync(int editionId, string featureName, string value)
        {
            return _featureValueStore.SetEditionFeatureValueAsync(editionId, featureName, value);
        }

        public virtual async Task<IReadOnlyList<NameValue>> GetFeatureValuesAsync(int editionId)
        {
            var values = new List<NameValue>();

            foreach (var feature in FeatureManager.GetAll())
            {
                values.Add(new NameValue(feature.Name, await GetFeatureValueOrNullAsync(editionId, feature.Name) ?? feature.DefaultValue));
            }

            return values;
        }

        public virtual async Task SetFeatureValuesAsync(int editionId, params NameValue[] values)
        {
            if (values.IsNullOrEmpty())
            {
                return;
            }

            foreach (var value in values)
            {
                await SetFeatureValueAsync(editionId, value.Name, value.Value);
            }
        }

        public virtual Task CreateAsync(Edition edition)
        {
            return EditionRepository.InsertAsync(edition);
        }

        public virtual Task<Edition> FindByNameAsync(string name)
        {
            return EditionRepository.FirstOrDefaultAsync(edition => edition.Name == name);
        }

        public virtual Task<Edition> FindByIdAsync(int id)
        {
            return EditionRepository.FirstOrDefaultAsync(id);
        }

        public virtual Task<Edition> GetByIdAsync(int id)
        {
            return EditionRepository.GetAsync(id);
        }

        public virtual Task DeleteAsync(Edition edition)
        {
            return EditionRepository.DeleteAsync(edition);
        }

        //TODO: Should move cache invalidation code to ApmFeatureValueStore

        public virtual void HandleEvent(EntityChangedEventData<EditionFeatureSetting> eventData)
        {
            CacheManager.GetEditionFeatureCache().Remove(eventData.Entity.EditionId);
        }

        public virtual void HandleEvent(EntityChangedEventData<Edition> eventData)
        {
            if (eventData.Entity.IsTransient())
            {
                return;
            }

            CacheManager.GetEditionFeatureCache().Remove(eventData.Entity.Id);
        }
    }
}
