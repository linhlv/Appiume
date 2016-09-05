using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Appiume.Apm.Runtime.Validation;
using Appiume.Web.Modules.TaskCloud.Core.Tasks;

namespace Appiume.Web.Modules.TaskCloud.Application.Tasks.Dtos
{
    /// <summary>
    /// This DTO class is used to send needed data to <see cref="ITaskAppService.UpdateTask"/> method.
    /// 
    /// Implements <see cref="ICustomValidate"/> for additional custom validation.
    /// </summary>
    public class UpdateTaskInput : ICustomValidate
    {
        /// <summary>
        /// 
        /// </summary>
        [Range(1, long.MaxValue)] //Data annotation attributes work as expected.
        public long TaskId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? AssignedPersonId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TaskState? State { get; set; }

        //Custom validation method. It's called by ABP after data annotation validations.
        public void AddValidationErrors(List<ValidationResult> results)
        {
            if (AssignedPersonId == null && State == null)
            {
                results.Add(new ValidationResult("Both of AssignedPersonId and State can not be null in order to update a Task!", new[] { "AssignedPersonId", "State" }));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[UpdateTaskInput > TaskId = {0}, AssignedPersonId = {1}, State = {2}]", TaskId, AssignedPersonId, State);
        }
    }
}
