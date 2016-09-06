using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Tenancy.Application.Editions;
using Appiume.Web.Dewey.Core.Features;

namespace Appiume.Web.Dewey.Core.Editions
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
