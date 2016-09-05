using System.ComponentModel.DataAnnotations;

namespace Appiume.Web.Modules.TaskCloud.Application.Tasks.Dtos
{
    /// <summary>
    ///
    /// </summary>
    public class CreateTaskInput
    {
        /// <summary>
        ///
        /// </summary>
        public int? AssignedPersonId { get; set; }

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
            return string.Format("[CreateTaskInput > AssignedPersonId = {0}, Description = {1}]", AssignedPersonId, Description);
        }
    }
}