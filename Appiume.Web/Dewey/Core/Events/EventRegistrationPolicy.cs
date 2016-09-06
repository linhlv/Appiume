using System;
using System.Threading.Tasks;
using Appiume.Apm.Configuration;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Timing;
using Appiume.Apm.UI;
using Appiume.Web.Dewey.Core.Configuration;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Events
{
    public class EventRegistrationPolicy : IoTServiceBase, IEventRegistrationPolicy
    {
        private readonly IRepository<EventRegistration> _eventRegistrationRepository;

        public EventRegistrationPolicy(IRepository<EventRegistration> eventRegistrationRepository)
        {
            _eventRegistrationRepository = eventRegistrationRepository;
        }

        public async Task CheckRegistrationAttemptAsync(Event @event, User user)
        {
            if (@event == null) { throw new ArgumentNullException("event"); }
            if (user == null) { throw new ArgumentNullException("user"); }

            CheckEventDate(@event);
            await CheckEventRegistrationFrequencyAsync(user);
        }

        private static void CheckEventDate(Event @event)
        {
            if (@event.IsInPast())
            {
                throw new UserFriendlyException("Can not register event in the past!"); //TODO: Localize
            }
        }

        private async Task CheckEventRegistrationFrequencyAsync(User user)
        {
            var oneMonthAgo = Clock.Now.AddDays(-30);
            var maxAllowedEventRegistrationCountInLast30DaysPerUser = await SettingManager.GetSettingValueAsync<int>(EventCloudSettingNames.MaxAllowedEventRegistrationCountInLast30DaysPerUser);
            if (maxAllowedEventRegistrationCountInLast30DaysPerUser > 0)
            {
                var registrationCountInLast30Days = await _eventRegistrationRepository.CountAsync(r => r.UserId == user.Id && r.CreationTime >= oneMonthAgo);
                if (registrationCountInLast30Days > maxAllowedEventRegistrationCountInLast30DaysPerUser)
                {
                    throw new UserFriendlyException(string.Format("Can not register to more than {0} events in 30 days", maxAllowedEventRegistrationCountInLast30DaysPerUser)); //TODO: Localize
                }
            }
        }
    }
}