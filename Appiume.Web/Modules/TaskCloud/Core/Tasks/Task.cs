using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Domain.Entities.Auditing;
using Appiume.Web.Modules.TaskCloud.Core.People;

namespace Appiume.Web.Modules.TaskCloud.Core.Tasks
{
    [Table("TaskCloudTask")]
    public class Task : Entity<long>, IHasCreationTime
    {
        /// <summary>
        /// A reference (navigation property) to assigned <see cref="Person"/> for this task.
        /// We declare <see cref="ForeignKeyAttribute"/> for EntityFramework here. No need for NHibernate.
        /// </summary>
        [ForeignKey("AssignedPersonId")]
        public virtual Person AssignedPerson { get; set; }


        /// <summary>
        /// Database field for AssignedPerson reference.
        /// Needed for EntityFramework, no need for NHibernate.
        /// </summary>
        public virtual int? AssignedPersonId { get; set; }


        /// <summary>
        /// Describes the task.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// The time when this task is created.
        /// </summary>
        public virtual DateTime CreationTime { get; set; }


        /// <summary>
        /// Current state of the task.
        /// </summary>
        public virtual TaskState State { get; set; }

        /// <summary>
        /// Default costructor.
        /// Assigns some default values to properties.
        /// </summary>
        public Task()
        {
            CreationTime = DateTime.Now;
            State = TaskState.Active;
        }
    }
}