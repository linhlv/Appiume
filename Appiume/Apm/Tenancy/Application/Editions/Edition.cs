using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Appiume.Apm.Domain.Entities.Auditing;
using Appiume.Apm.MultiTenancy;

namespace Appiume.Apm.Tenancy.Application.Editions
{
    /// <summary>
    /// Represents an edition of the application.
    /// </summary>
    [Table("ApmEditions")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class Edition : FullAuditedEntity
    {
        /// <summary>
        /// Maximum length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// Maximum length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// Unique name of this edition.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Display name of this edition.
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        public Edition()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        public Edition(string displayName)
            : this()
        {
            DisplayName = displayName;
        }
    }
}