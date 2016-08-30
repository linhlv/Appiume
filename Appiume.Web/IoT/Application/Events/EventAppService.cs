using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.Authorization;
using Appiume.Apm.AutoMapper;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Linq.Extensions;
using Appiume.Apm.UI;
using Appiume.Web.IoT.Application.Events.Dtos;
using Appiume.Web.IoT.Core;
using Appiume.Web.IoT.Core.Events;
using Appiume.Web.IoT.Core.Users;
using Appiume.Apm.Runtime.Session;

namespace Appiume.Web.IoT.Application.Events
{
    [ApmAuthorize]
    public class EventAppService : IoTAppServiceBase, IEventAppService
    {
        private readonly IEventManager _eventManager;
        private readonly IRepository<Event, Guid> _eventRepository;

        public EventAppService(
            IEventManager eventManager, 
            IRepository<Event, Guid> eventRepository)
        {
            _eventManager = eventManager;
            _eventRepository = eventRepository;
        }

        public async Task<ListResultOutput<EventListDto>> GetList(GetEventListInput input)
        {
            var events = await _eventRepository
                .GetAll()
                .Include(e => e.Registrations)
                .WhereIf(!input.IncludeCanceledEvents, e => !e.IsCancelled)
                .OrderByDescending(e => e.CreationTime)
                .ToListAsync();

            return new ListResultOutput<EventListDto>(events.MapTo<List<EventListDto>>());
        }

        public async Task<EventDetailOutput> GetDetail(EntityRequestInput<Guid> input)
        {
            var @event = await _eventRepository
                .GetAll()
                .Include(e => e.Registrations)
                .Where(e => e.Id == input.Id)
                .FirstOrDefaultAsync();

            if (@event == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted.");
            }

            return @event.MapTo<EventDetailOutput>();
        }

        public async Task Create(CreateEventInput input)
        {
            var @event = Event.Create(ApmSession.GetTenantId(), input.Title, input.Date, input.Description, input.MaxRegistrationCount);
            await _eventManager.CreateAsync(@event);
        }

        public async Task Cancel(EntityRequestInput<Guid> input)
        {
            var @event = await _eventManager.GetAsync(input.Id);
            _eventManager.Cancel(@event);
        }

        public async Task<EventRegisterOutput> Register(EntityRequestInput<Guid> input)
        {
            var registration = await RegisterAndSaveAsync(
                await _eventManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );

            return new EventRegisterOutput
            {
                RegistrationId = registration.Id
            };
        }

        public async Task CancelRegistration(EntityRequestInput<Guid> input)
        {
            await _eventManager.CancelRegistrationAsync(
                await _eventManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );
        }

        private async Task<EventRegistration> RegisterAndSaveAsync(Event @event, User user)
        {
            var registration = await _eventManager.RegisterAsync(@event, user);
            await CurrentUnitOfWork.SaveChangesAsync();
            return registration;
        }
    }
}