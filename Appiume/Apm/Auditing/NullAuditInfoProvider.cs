﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Auditing
{
    /// <summary>
    /// Null implementation of <see cref="IAuditInfoProvider"/>.
    /// </summary>
    internal class NullAuditInfoProvider : IAuditInfoProvider
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullAuditInfoProvider Instance { get { return SingletonInstance; } }
        private static readonly NullAuditInfoProvider SingletonInstance = new NullAuditInfoProvider();

        public void Fill(AuditInfo auditInfo)
        {

        }
    }
}