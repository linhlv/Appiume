using Appiume.Apm.Events.Bus.Entities;

namespace Appiume.Web.IoT.Core.Events
{
    public class EventDateChangedEvent : EntityEventData<Event>
    {
        public EventDateChangedEvent(Event entity)
            : base(entity)
        {
        }
    }
}