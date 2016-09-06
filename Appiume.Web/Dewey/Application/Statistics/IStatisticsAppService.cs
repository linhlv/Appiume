using System.Threading.Tasks;
using Appiume.Apm.Application.Services;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Statistics
{
    public interface IStatisticsAppService : IApplicationService
    {
        Task<ListResultOutput<NameValueDto>> GetStatistics();
    }
}