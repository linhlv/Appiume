using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.Runtime.Validation;
using Appiume.Web.Dewey.Core.Tasks;

namespace Appiume.Web.Dewey.Application.Tasks.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTasksInput : IOutputDto, IPagedResultRequest, ICustomValidate
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TaskDto Task { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TaskState? State { get; set; }

        public List<TaskState> TaskStates { get; set; }

        public int SkipCount { get; set; }

        public int MaxResultCount { get; set; }

        public GetTasksInput()
        {
            MaxResultCount = int.MaxValue;
            TaskStates = new List<TaskState>();
        }

        public void AddValidationErrors(List<ValidationResult> results)
        {
            //TODO: For demonstration, do it declarative!
            if (AssignedUserId <= 0)
            {
                results.Add(new ValidationResult("AssignedUserId must be a positive value!", new[] { "AssignedUserId" }));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long? AssignedUserId { get; set; }
    }
}