﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm
{
    /// <summary>
    /// Interface to get a user identifier.
    /// </summary>
    public interface IUserIdentifier
    {
        /// <summary>
        /// Tenant Id. Can be null for host users.
        /// </summary>
        int? TenantId { get; }

        /// <summary>
        /// Id of the user.
        /// </summary>
        long UserId { get; }
    }
}
