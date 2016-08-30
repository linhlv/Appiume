using System;

namespace Appiume.Apm.Runtime.Session
{
    /// <summary>
    /// Extension methods for <see cref="IApmSession"/>.
    /// </summary>
    public static class ApmSessionExtensions
    {
        /// <summary>
        /// Gets current User's Id.
        /// Throws <see cref="ApmException"/> if <see cref="IApmSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current User's Id.</returns>
        public static long GetUserId(this IApmSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new ApmException("Session.UserId is null! Probably, user is not logged in.");
            }

            return session.UserId.Value;
        }

        /// <summary>
        /// Gets current Tenant's Id.
        /// Throws <see cref="ApmException"/> if <see cref="IApmSession.TenantId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current Tenant's Id.</returns>
        /// <exception cref="ApmException"></exception>
        public static int GetTenantId(this IApmSession session)
        {
            if (!session.TenantId.HasValue)
            {
                throw new ApmException("Session.TenantId is null! Possible problems: No user logged in or current logged in user in a host user (TenantId is always null for host users).");
            }

            return session.TenantId.Value;
        }

        /// <summary>
        /// Creates <see cref="UserIdentifier"/> from given session.
        /// Returns null if <see cref="IApmSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">The session.</param>
        public static UserIdentifier ToUserIdentifier(this IApmSession session)
        {
            return session.UserId == null
                ? null
                : new UserIdentifier(session.TenantId, session.GetUserId());
        }
    }
}