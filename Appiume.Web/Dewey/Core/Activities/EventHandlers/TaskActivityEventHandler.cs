using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Dependency;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Events.Bus.Entities;
using Appiume.Apm.Events.Bus.Handlers;
using Appiume.Web.Dewey.Core.Tasks.Events;
using Appiume.Web.Dewey.Core.Users;
using Appiume.Web.Dewey.Core.Tasks;

namespace Appiume.Web.Dewey.Core.Activities.EventHandlers
{
    public class TaskActivityEventHandler :
       IEventHandler<EntityCreatedEventData<Task>>,
       IEventHandler<TaskCompletedEventData>,
       ITransientDependency
    {
        private readonly IActivityService _activityService;
        private readonly IRepository<User, long> _userRepository;

        public TaskActivityEventHandler(IActivityService activityService, IRepository<User, long> userRepository)
        {
            _activityService = activityService;
            _userRepository = userRepository;
        }

        public void HandleEvent(EntityCreatedEventData<Task> eventData)
        {
            var activity = new CreateTaskActivity
            {
                CreatorUser =
                                   eventData.Entity.CreatorUserId.HasValue
                                       ? _userRepository.Load(eventData.Entity.CreatorUserId.Value)
                                       : null,
                AssignedUser = eventData.Entity.AssignedUser,
                Task = eventData.Entity
            };


            //activity.AssignedUserId = activity.AssignedUser.Id;
            //activity.CreatorUserId = activity.CreatorUser.Id;
            //activity.TaskId = activity.Task.Id;

            _activityService.AddActivity(activity);
        }
        public void HandleEvent(TaskCompletedEventData eventData)
        {
            _activityService.AddActivity(
                    new CompleteTaskActivity
                    {
                        AssignedUser = eventData.Entity.AssignedUser,
                        Task = eventData.Entity
                    });
        }
    }
}