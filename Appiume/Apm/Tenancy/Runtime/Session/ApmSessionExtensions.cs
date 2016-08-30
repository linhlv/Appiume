using System;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.Tenancy.Authorization.Users;

namespace Appiume.Apm.Tenancy.Runtime.Session
{
    public static class ApmSessionExtensions
    {
        public static bool IsUser(this IApmSession session, ApmUserBase user)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return session.TenantId == user.TenantId && 
                session.UserId.HasValue && 
                session.UserId.Value == user.Id;
        }
    }
}
