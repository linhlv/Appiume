using System.Threading.Tasks;
using Appiume.Apm.Application.Features;

namespace Appiume.Apm.Tenancy.Application.Features
{
    public interface IApmTenancyFeatureValueStore : IFeatureValueStore
    {
        Task<string> GetValueOrNullAsync(int tenantId, string featureName);
        Task<string> GetEditionValueOrNullAsync(int editionId, string featureName);
        Task SetEditionFeatureValueAsync(int editionId, string featureName, string value);
    }
}