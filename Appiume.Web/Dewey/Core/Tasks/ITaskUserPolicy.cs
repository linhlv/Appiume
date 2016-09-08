using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Domain.Policies;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Tasks
{
    public interface ITaskUserPolicy : IPolicy
    {
        bool CanSeeProfile(ApmUser<User> requesterUser, ApmUser<User> targetUser);
    }
}