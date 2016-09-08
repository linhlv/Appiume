using System;
using System.ComponentModel.DataAnnotations;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Web.Dewey.Core.Tasks;

namespace Appiume.Web.Dewey.Application.Tasks.Dtos
{
    /// <summary>
    /// A DTO class that can be used in various application service methods when needed to send/receive Task objects.
    /// </summary>
    public class TaskDto : EntityDto<long>
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }

        public long? AssignedUserId { get; set; }

        public string AssignedUserName { get; set; }

        public byte Priority { get; set; }
        
        public byte Privacy { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 1)]
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public byte State { get; set; }

        //This method is just used by the Console Application to list tasks
        public override string ToString()
        {
            return string.Format(
                "[Task Id={0}, Description={1}, CreationTime={2}, AssignedUserName={3}, State={4}]",
                Id,
                Description,
                CreationTime,
                AssignedUserId,
                (TaskState)State
                );
        }
    }
}