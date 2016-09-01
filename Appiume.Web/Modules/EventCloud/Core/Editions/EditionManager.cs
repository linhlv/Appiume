using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Tenancy.Application.Editions;
using Appiume.Web.Modules.EventCloud.Core.Features;

namespace Appiume.Web.Modules.EventCloud.Core.Editions
{
    public class EditionManager : ApmEditionManager
    {
        public EditionManager(
            IRepository<Edition> editionRepository,
            FeatureValueStore featureValueStore)
            : base(
                editionRepository,
                featureValueStore)
        {
        }
    }
}
