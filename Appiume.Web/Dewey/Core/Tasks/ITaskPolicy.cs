using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Domain.Policies;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Tasks
{
    //TODO: Renamt this to Policy ?
    public interface ITaskPolicy : IPolicy
    {
        bool CanSeeTasksOfUser(ApmUser<User> requesterUser, ApmUser<User> userOfTasks);

        bool CanAssignTask(ApmUser<User> assignerUser, ApmUser<User> userToAssign);

        bool CanUpdateTask(ApmUser<User> user, Task task);

        bool CanDeleteTask(ApmUser<User> user, Task task);
    }
}