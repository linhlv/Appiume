using Appiume.Apm.Dependency;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Events.Bus.Entities;
using Appiume.Apm.Events.Bus.Handlers;
using Appiume.Web.Dewey.Core.Notifications.Tasks;
using Appiume.Web.Dewey.Core.Tasks;
using Appiume.Web.Dewey.Core.Tasks.Events;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Notifications.EventHandlers
{
    public class TaskNotificationEventHandler : 
        IEventHandler<EntityCreatedEventData<Task>>, 
        IEventHandler<TaskCompletedEventData>,
        ITransientDependency
    {
        private readonly INotificationService _notificationService;

        private readonly IRepository<User, long> _userRepository;
        
        public TaskNotificationEventHandler(INotificationService notificationService, IRepository<User, long> userRepository)
        {
            _notificationService = notificationService;
            _userRepository = userRepository;
        }

        public void HandleEvent(EntityCreatedEventData<Task> eventData)
        {
            if (eventData.Entity.AssignedUser.Id != eventData.Entity.CreatorUserId)
            {
                _notificationService.Notify(new AssignedToTaskNotification(eventData.Entity));
            }
        }

        public void HandleEvent(TaskCompletedEventData eventData)
        {
            if (eventData.Entity.CreatorUserId.HasValue)
            {
                _notificationService.Notify(new CompletedTaskNotification(eventData.Entity, _userRepository.Get(eventData.Entity.CreatorUserId.Value)));
            }
        }
    }
}
