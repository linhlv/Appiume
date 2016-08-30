using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Tenancy.Application.Editions;
using Appiume.Web.IoT.Core.Features;

namespace Appiume.Web.IoT.Core.Editions
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
