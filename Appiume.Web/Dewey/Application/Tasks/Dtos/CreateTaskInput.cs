using System.ComponentModel.DataAnnotations;
using System.Web;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.Runtime.Validation;

namespace Appiume.Web.Dewey.Application.Tasks.Dtos
{
    /// <summary>
    ///
    /// </summary>
    public class CreateTaskInput : IInputDto, IShouldNormalize
    {
        public TaskDto Task { get; set; }

        /// <summary>
        ///
        /// </summary>
        public long? AssignedUserId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[CreateTaskInput > AssignedUserId = {0}, Description = {1}]", AssignedUserId, Description);
        }

        public void Normalize()
        {
            Task.Title = HttpUtility.HtmlEncode(Task.Title);
            Task.Description = HttpUtility.HtmlEncode(Task.Description);
        }
    }
}