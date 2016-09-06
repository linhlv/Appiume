using Appiume.Apm.Events.Bus;

namespace Appiume.Web.Dewey.Core.Domain.Events
{
    public static class DomainEvents
    {
        public static IEventBus EventBus { get; set; }

        static DomainEvents()
        {
            EventBus = Appiume.Apm.Events.Bus.EventBus.Default;
        }
    }
}
