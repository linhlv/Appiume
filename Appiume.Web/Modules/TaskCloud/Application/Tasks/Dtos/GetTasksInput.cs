using Appiume.Web.Modules.TaskCloud.Core.Tasks;

namespace Appiume.Web.Modules.TaskCloud.Application.Tasks.Dtos
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