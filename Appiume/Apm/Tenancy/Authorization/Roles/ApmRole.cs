﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Appiume.Apm.Domain.Entities.Auditing;
using Appiume.Apm.Tenancy.Authorization.Users;

namespace Appiume.Apm.Tenancy.Authorization.Roles
{
    /// <summary>
    /// Represents a role in an application. A role is used to group permissions.
    /// </summary>
    /// <remarks> 
    /// Application should use permissions to check if user is granted to perform an operation.
    /// Checking 'if a user has a role' is not possible until the role is static (<see cref="IsStatic"/>).
    /// Static roles can be used in the code and can not be deleted by users.
    /// Non-static (dynamic) roles can be added/removed by users and we can not know their name while coding.
    /// A user can have multiple roles. Thus, user will have all permissions of all assigned roles.
    /// </remarks>
    public abstract class ApmRole<TUser> : ApmRoleBase, IFullAudited<TUser>
        where TUser : ApmUser<TUser>
    {
        /// <summary>
        /// Maximum length of the <see cref="DisplayName"/> property.
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// Display name of this role.
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Is this a static role?
        /// Static roles can not be deleted, can not change their name.
        /// They can be used programmatically.
        /// </summary>
        public virtual bool IsStatic { get; set; }

        /// <summary>
        /// Is this role will be assigned to new users as default?
        /// </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>
        /// List of permissions of the role.
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<RolePermissionSetting> Permissions { get; set; }

        public virtual TUser DeleterUser { get; set; }

        public virtual TUser CreatorUser { get; set; }

        public virtual TUser LastModifierUser { get; set; }

        /// <summary>
        /// Creates a new <see cref="ApmRole{TTenant,TUser}"/> object.
        /// </summary>
        public ApmRole()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Creates a new <see cref="ApmRole{TTenant,TUser}"/> object.
        /// </summary>
        /// <param name="tenantId">TenantId or null (if this is not a tenant-level role)</param>
        /// <param name="displayName">Display name of the role</param>
        public ApmRole(int? tenantId, string displayName)
            : this()
        {
            TenantId = tenantId;
            DisplayName = displayName;
        }

        /// <summary>
        /// Creates a new <see cref="ApmRole{TTenant,TUser}"/> object.
        /// </summary>
        /// <param name="tenantId">TenantId or null (if this is not a tenant-level role)</param>
        /// <param name="name">Unique role name</param>
        /// <param name="displayName">Display name of the role</param>
        public ApmRole(int? tenantId, string name, string displayName)
            : this(tenantId, displayName)
        {
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("[Role {0}, Name={1}]", Id, Name);
        }
    }
}
