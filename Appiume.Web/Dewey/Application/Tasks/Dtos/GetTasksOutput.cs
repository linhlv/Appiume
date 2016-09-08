using System.Collections.Generic;

namespace Appiume.Web.Dewey.Application.Tasks.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetTasksOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<TaskDto> Tasks { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IsEditableByCurrentUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeletableByCurrentUser { get; set; }
    }
}