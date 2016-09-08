
using Appiume.Apm.Application.Services;
using Appiume.Web.Dewey.Application.Tasks.Dtos;
using Appiume.Web.Dewey.Core.Tasks;

namespace Appiume.Web.Dewey.Application.Tasks
{
    /// <summary>
    /// Defines an application service for <see cref="Task"/> operations.
    /// 
    /// It extends <see cref="IApplicationService"/>.
    /// Thus, ABP enables automatic dependency injection, validation and other benefits for it.
    /// 
    /// Application services works with DTOs (Data Transfer Objects).
    /// Service methods gets and returns DTOs.
    /// </summary>
    public interface ITaskAppService : IApplicationService
    {
        GetTaskOutput GetTask(GetTaskInput input);

        GetTasksOutput GetTasks(GetTasksInput input);

        GetTasksByImportanceOutput GetTasksByImportance(GetTasksByImportanceInput input);

        CreateTaskOutput CreateTask(CreateTaskInput input);

        void UpdateTask(UpdateTaskInput input);

        DeleteTaskOutput DeleteTask(DeleteTaskInput input);
    }
}
