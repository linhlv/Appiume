﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Domain.Entities.Auditing;
using Appiume.Web.Dewey.Core.People;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Core.Tasks
{
    [Table("DeweyTask")]
    public class Task : AuditedEntity<long>, IHasCreationTime
    {
        /// <summary>
        /// Task title.
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// A reference (navigation property) to assigned <see cref="Person"/> for this task.
        /// We declare <see cref="ForeignKeyAttribute"/> for EntityFramework here. No need for NHibernate.
        /// </summary>
        [ForeignKey("AssignedUserId")]
        public virtual User AssignedPerson { get; set; }


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
        /// Default costructor.
        /// Assigns some default values to properties.
        /// </summary>
        public Task()
        {
            CreationTime = DateTime.Now;
            State = TaskState.Active;
        }



        [ForeignKey("AssignedUserId")]
        public virtual User AssignedUser { get; set; }

        public virtual long? AssignedUserId { get; set; }

        public virtual TaskPriority Priority { get; set; }

        public virtual TaskPrivacy Privacy { get; set; }

        public virtual TaskState State { get; set; }
    }
}