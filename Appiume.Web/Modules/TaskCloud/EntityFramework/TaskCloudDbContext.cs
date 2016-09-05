using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Appiume.Apm.Ef;
using Appiume.Web.Modules.TaskCloud.Core.People;
using Appiume.Web.Modules.TaskCloud.Core.Tasks;

namespace Appiume.Web.Modules.TaskCloud.EntityFramework
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskCloudDbContext : ApmDbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Task> Tasks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Person> People { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TaskCloudDbContext()
            : base("Default")
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public TaskCloudDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// This constructor is used in tests
        /// </summary>
        /// <param name="connection"></param>
        public TaskCloudDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}