﻿using System.ComponentModel.DataAnnotations;
using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Domain.Entities.Auditing;
using Appiume.Apm.Tenancy.Application.Editions;
using Appiume.Apm.Tenancy.Authorization.Users;

namespace Appiume.Apm.Tenancy.MultiTenancy
{
    /// <summary>
    /// Represents a Tenant of the application.
    /// </summary>
    public abstract class ApmTenant<TUser> : ApmTenantBase, IFullAudited<TUser>, IPassivable
        where TUser : ApmUser<TUser>
    {
        /// <summary>
        /// "Default".
        /// </summary>
        public const string DefaultTenantName = "Default";

        /// <summary>
        /// "^[a-zA-Z][a-zA-Z0-9_-]{1,}$".
        /// </summary>
        public const string TenancyNameRegex = "^[a-zA-Z][a-zA-Z0-9_-]{1,}$";
        
        /// <summary>
        /// Max length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxNameLength = 128;

        /// <summary>
        /// Current <see cref="Edition"/> of the Tenant.
        /// </summary>
        public virtual Edition Edition { get; set; }
        public virtual int? EditionId { get; set; }

        /// <summary>
        /// Display name of the Tenant.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Is this tenant active?
        /// If as tenant is not active, no user of this tenant can use the application.
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Reference to the creator user of this entity.
        /// </summary>
        public virtual TUser CreatorUser { get; set; }

        /// <summary>
        /// Reference to the last modifier user of this entity.
        /// </summary>
        public virtual TUser LastModifierUser { get; set; }

        /// <summary>
        /// Reference to the deleter user of this entity.
        /// </summary>
        public virtual TUser DeleterUser { get; set; }

        /// <summary>
        /// Creates a new tenant.
        /// </summary>
        protected ApmTenant()
        {
            IsActive = true;
        }

        /// <summary>
        /// Creates a new tenant.
        /// </summary>
        /// <param name="tenancyName">UNIQUE name of this Tenant</param>
        /// <param name="name">Display name of the Tenant</param>
        protected ApmTenant(string tenancyName, string name)
            : this()
        {
            TenancyName = tenancyName;
            Name = name;
        }
    }
}
