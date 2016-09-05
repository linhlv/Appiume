using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Appiume.Apm.Domain.Entities;

namespace Appiume.Web.Modules.TaskCloud.Core.People
{
    /// <summary>
    /// 
    /// </summary>
    [Table("TaskCloudTask")]
    public class Person : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }
    }
}