using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Tasks.Dtos
{
    public class GetTaskOutput : IOutputDto
    {
        public TaskWithAssignedUserDto Task { get; set; }

        public bool IsEditableByCurrentUser { get; set; }

        public bool IsDeletableByCurrentUser { get; set; }
    }
}