using Appiume.Web.Dewey.Core.Tasks;

namespace Appiume.Web.Dewey.Application.Tasks.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTasksInput
    {
        /// <summary>
        /// 
        /// </summary>
        public TaskState? State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? AssignedPersonId { get; set; }
    }
}