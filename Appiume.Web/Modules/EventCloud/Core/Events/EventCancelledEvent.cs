using Appiume.Apm.Events.Bus.Entities;

namespace Appiume.Web.Modules.EventCloud.Core.Events
{
    public class EventCancelledEvent : EntityEventData<Event>
    {
        public EventCancelledEvent(Event entity) 
            : base(entity)
        {
        }
    }
}