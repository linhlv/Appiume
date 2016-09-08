using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Tasks.Dtos
{
    public class CreateTaskOutput : IOutputDto
    {
        public TaskDto Task { get; set; }
    }
}