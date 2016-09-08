using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Appiume.Apm.Runtime.Validation;
using Appiume.Web.Dewey.Core.Tasks;

namespace Appiume.Web.Dewey.Application.Tasks.Dtos
{
    /// <summary>
    /// This DTO class is used to send needed data to <see cref="ITaskAppService.UpdateTask"/> method.
    /// 
    /// Implements <see cref="ICustomValidate"/> for additional custom validation.
    /// </summary>
    public class UpdateTaskInput : ICustomValidate
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public byte Priority { get; set; }

        public byte Privacy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Range(1, long.MaxValue)] //Data annotation attributes work as expected.
        public long TaskId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? AssignedUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TaskState? State { get; set; }

        //Custom validation method. It's called by ABP after data annotation validations.
        public void AddValidationErrors(List<ValidationResult> results)
        {
            if (AssignedUserId == null && State == null)
            {
                results.Add(new ValidationResult("Both of AssignedUserId and State can not be null in order to update a Task!", new[] { "AssignedUserId", "State" }));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[UpdateTaskInput > TaskId = {0}, AssignedUserId = {1}, State = {2}]", TaskId, AssignedUserId, State);
        }
    }
}
