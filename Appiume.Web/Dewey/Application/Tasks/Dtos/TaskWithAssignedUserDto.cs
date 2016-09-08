using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Web.Dewey.Application.Users.Dto;

namespace Appiume.Web.Dewey.Application.Tasks.Dtos
{
    public class TaskWithAssignedUserDto : TaskDto
    {
        public virtual UserDto AssignedUser { get; set; }
    }
}