using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.MultiTenancy;

namespace Appiume.Apm.Runtime.Session
{
    /// <summary>
    /// Implements null object pattern for <see cref="IApmSession"/>.
    /// </summary>
    public class NullApmSession : IApmSession
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullApmSession Instance { get { return SingletonInstance; } }
        private static readonly NullApmSession SingletonInstance = new NullApmSession();

        /// <inheritdoc/>
        public long? UserId { get { return null; } }

        /// <inheritdoc/>
        public int? TenantId { get { return null; } }

        public MultiTenancySides MultiTenancySide { get { return MultiTenancySides.Tenant; } }

        public long? ImpersonatorUserId { get { return null; } }

        public int? ImpersonatorTenantId { get { return null; } }

        private NullApmSession()
        {

        }
    }
}