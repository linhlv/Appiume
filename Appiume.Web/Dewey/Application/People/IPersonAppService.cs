using System.Threading.Tasks;
using Appiume.Apm.Application.Services;
using Appiume.Web.Dewey.Application.People.Dtos;

namespace Appiume.Web.Dewey.Application.People
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
