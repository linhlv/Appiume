﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Domain.Entities.Auditing;

namespace Appiume.Apm.Tenancy.Authorization
{
    /// <summary>
    /// Used to grant/deny a permission for a role or user.
    /// </summary>
    [Table("ApmPermissions")]
    public abstract class PermissionSetting : CreationAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// Maximum length of the <see cref="Name"/> field.
        /// </summary>
        public const int MaxNameLength = 128;

        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Unique name of the permission.
        /// </summary>
        [Required]
        [MaxLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Is this role granted for this permission.
        /// Default value: true.
        /// </summary>
        public virtual bool IsGranted { get; set; }

        /// <summary>
        /// Creates a new <see cref="PermissionSetting"/> entity.
        /// </summary>
        protected PermissionSetting()
        {
            IsGranted = true;
        }
    }
}
