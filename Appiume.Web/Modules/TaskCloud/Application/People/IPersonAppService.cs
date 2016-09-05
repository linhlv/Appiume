using System.Threading.Tasks;
using Appiume.Apm.Application.Services;
using Appiume.Web.Modules.TaskCloud.Application.People.Dtos;

namespace Appiume.Web.Modules.TaskCloud.Application.People
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPersonAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<GetAllPeopleOutput> GetAllPeople();
    }
}
