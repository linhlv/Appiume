using System;
using System.Threading.Tasks;
using Appiume.Apm.Application.Services;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Web.Modules.EventCloud.Application.Events.Dtos;

namespace Appiume.Web.Modules.EventCloud.Application.Events
{
    public interface IEventAppService : IApplicationService
    {
        Task<ListResultOutput<EventListDto>> GetList(GetEventListInput input);

        Task<EventDetailOutput> GetDetail(EntityRequestInput<Guid> input);

        Task Create(CreateEventInput input);

        Task Cancel(EntityRequestInput<Guid> input);

        Task<EventRegisterOutput> Register(EntityRequestInput<Guid> input);

        Task CancelRegistration(EntityRequestInput<Guid> input);
    }
}
