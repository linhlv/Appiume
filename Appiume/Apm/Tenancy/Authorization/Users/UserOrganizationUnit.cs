﻿using System.ComponentModel.DataAnnotations.Schema;
using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Domain.Entities.Auditing;
using Appiume.Apm.Tenancy.Organizations;

namespace Appiume.Apm.Tenancy.Authorization.Users
{
    /// <summary>
    /// Represents membership of a User to an OU.
    /// </summary>
    [Table("ApmUserOrganizationUnits")]
    public class UserOrganizationUnit : CreationAuditedEntity<long>, IMayHaveTenant
    {
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Id of the User.
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// Id of the <see cref="OrganizationUnit"/>.
        /// </summary>
        public virtual long OrganizationUnitId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserOrganizationUnit"/> class.
        /// </summary>
        public UserOrganizationUnit()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserOrganizationUnit"/> class.
        /// </summary>
        /// <param name="tenantId">TenantId</param>
        /// <param name="userId">Id of the User.</param>
        /// <param name="organizationUnitId">Id of the <see cref="OrganizationUnit"/>.</param>
        public UserOrganizationUnit(int? tenantId, long userId, long organizationUnitId)
        {
            TenantId = tenantId;
            UserId = userId;
            OrganizationUnitId = organizationUnitId;
        }
    }
}
