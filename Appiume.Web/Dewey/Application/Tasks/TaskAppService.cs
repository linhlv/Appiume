using System;
using System.Collections.Generic;
using System.Linq;
using Appiume.Apm.Application.Services;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Events.Bus;
using Appiume.Apm.Events.Bus.Entities;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.UI;
using Appiume.Web.Dewey.Application.Mapping;
using Appiume.Web.Dewey.Application.Tasks.Dtos;
using Appiume.Web.Dewey.Core.People;
using Appiume.Web.Dewey.Core.Tasks;
using Appiume.Web.Dewey.Core.Tasks.Events;
using Appiume.Web.Dewey.Core.Users;
using AutoMapper;

namespace Appiume.Web.Dewey.Application.Tasks
{
    /// <summary>
    /// Implements <see cref="ITaskAppService"/> to perform task related application functionality.
    ///
    /// Inherits from <see cref="ApplicationService"/>.
    /// <see cref="ApplicationService"/> contains some basic functionality common for application services (such as logging and localization).
    /// </summary>
    public class TaskAppService : ApplicationService, ITaskAppService
    {
        //These members set in constructor using constructor injection.

        private readonly ITaskPolicy _taskPolicy;
        private readonly ITaskRepository _taskRepository;
        private readonly IRepository<User,long> _userRepository;
        private readonly IEventBus _eventBus;

        /// <summary>
        ///In constructor, we can get needed classes/interfaces.
        ///They are sent here by dependency injection system automatically.
        /// </summary>
        public TaskAppService(ITaskPolicy taskPolicy, 
            ITaskRepository taskRepository, 
            IRepository<User, long> userRepository,
            IEventBus eventBus)
        {
            _taskPolicy = taskPolicy;
            _eventBus = eventBus;
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public GetTaskOutput GetTask(GetTaskInput input)
        {
            var currentUser = _userRepository.Load(ApmSession.GetUserId());
            var task = _taskRepository.FirstOrDefault(input.Id);

            if (task == null)
            {
                throw new UserFriendlyException("Can not found the task!");
            }

            if (!_taskPolicy.CanSeeTasksOfUser(currentUser, task.AssignedUser))
            {
                throw new UserFriendlyException("Can not see tasks of " + task.AssignedUser.Name);
            }

            if (task.AssignedUser.Id != currentUser.Id && task.Privacy == TaskPrivacy.Private)
            {
                throw new UserFriendlyException("Can not see this task since it's private!");
            }

            return new GetTaskOutput
            {
                Task = task.MapTo<TaskWithAssignedUserDto>(),
                IsEditableByCurrentUser = _taskPolicy.CanUpdateTask(currentUser, task)
            };
        }

        public GetTasksOutput GetTasks(GetTasksInput input)
        {

            var query = CreateQueryForAssignedTasksOfUser(input.AssignedUserId.Value);
            if (!input.TaskStates.IsNullOrEmpty())
            {
                query = query.Where(task => input.TaskStates.Contains(task.State));
            }

            //Called specific GetAllWithPeople method of task repository.
            //var tasks = _taskRepository.GetAllWithPeople(input.AssignedUserId, input.State);

            //Used AutoMapper to automatically convert List<Task> to List<TaskDto>.
            return new GetTasksOutput
                   {
                        Tasks = query.ToList().MapIList<Task, TaskDto>()
                    
            };
        }

        public void UpdateTask(UpdateTaskInput input)
        {
            var task = _taskRepository.FirstOrDefault(input.Id);
            if (task == null)
            {
                throw new Exception("Can not found the task!");
            }

            var currentUser = _userRepository.Load(ApmSession.GetUserId()); //TODO: Add method LoadCurrentUser and GetCurrentUser ???
            if (!_taskPolicy.CanUpdateTask(currentUser, task))
            {
                throw new UserFriendlyException("You can not update this task!");
            }

            if (task.AssignedUser.Id != input.AssignedUserId)
            {
                var userToAssign = _userRepository.Load(input.AssignedUserId.Value);

                if (!_taskPolicy.CanAssignTask(currentUser, userToAssign))
                {
                    throw new UserFriendlyException("You can not assign task to this user!");
                }

                task.AssignedUser = userToAssign;
            }

            var oldTaskState = task.State;

            //TODO: Can we use Auto mapper?

            task.Description = input.Description;
            task.Priority = (TaskPriority)input.Priority;
            task.State = (TaskState)input.State;
            task.Privacy = (TaskPrivacy)input.Privacy;
            task.Title = input.Title;

            if (oldTaskState != TaskState.Completed && task.State == TaskState.Completed)
            {
                _eventBus.Trigger(this, new TaskCompletedEventData(task));
            }
        }

        public DeleteTaskOutput DeleteTask(DeleteTaskInput input)
        {
            var task = _taskRepository.FirstOrDefault(input.Id);
            if (task == null)
            {
                throw new Exception("Can not found the task!");
            }

            var currentUser = _userRepository.Load(ApmSession.GetUserId());
            if (!_taskPolicy.CanDeleteTask(currentUser, task))
            {
                throw new UserFriendlyException("You can not delete this task!");
            }

            _taskRepository.Delete(task);

            return new DeleteTaskOutput();
        }

        public GetTasksByImportanceOutput GetTasksByImportance(GetTasksByImportanceInput input)
        {
            var query = CreateQueryForAssignedTasksOfUser(input.AssignedUserId);
            query = query
                .Where(task => task.State != TaskState.Completed)
                .OrderByDescending(task => task.Priority)
                .ThenByDescending(task => task.State)
                .ThenByDescending(task => task.CreationTime)
                .Take(input.MaxResultCount);

            return new GetTasksByImportanceOutput
            {
                Tasks = query.ToList().MapIList<Task, TaskDto>()
            };
        }

        public virtual CreateTaskOutput CreateTask(CreateTaskInput input)
        {
            //Get entities from database
            var creatorUser = _userRepository.Get(ApmSession.GetUserId());
            var assignedUser = _userRepository.Get(input.Task.AssignedUserId.Value);

            if (!_taskPolicy.CanAssignTask(creatorUser, assignedUser))
            {
                throw new UserFriendlyException("You can not assign task to this user!");
            }

            //Create the task
            var taskEntity = input.Task.MapTo<Task>();

            taskEntity.CreatorUserId = creatorUser.Id;
            taskEntity.AssignedUser = _userRepository.Load(input.Task.AssignedUserId.Value);
            taskEntity.State = TaskState.New;

            if (taskEntity.AssignedUser.Id != creatorUser.Id && taskEntity.Privacy == TaskPrivacy.Private)
            {
                throw new ApplicationException("A user can not assign a private task to another user!");
            }

            _taskRepository.Insert(taskEntity);

            Logger.Debug("Creaded " + taskEntity);

            _eventBus.Trigger(this, new EntityCreatedEventData<Task>(taskEntity));

            return new CreateTaskOutput
            {
                Task = taskEntity.MapTo<TaskDto>()
            };
        }

        private IQueryable<Task> CreateQueryForAssignedTasksOfUser(long assignedUserId)
        {
            var currentUser = _userRepository.Load(ApmSession.GetUserId());
            var userOfTasks = _userRepository.Load(assignedUserId);

            if (!_taskPolicy.CanSeeTasksOfUser(currentUser, userOfTasks))
            {
                throw new ApplicationException("Can not see tasks of user");
            }

            var query = _taskRepository
                .GetAll()
                .Where(task => task.AssignedUser.Id == assignedUserId);

            if (currentUser.Id != userOfTasks.Id)
            {
                query = query.Where(task => task.Privacy != TaskPrivacy.Private);
            }

            return query;
        }
    }
}