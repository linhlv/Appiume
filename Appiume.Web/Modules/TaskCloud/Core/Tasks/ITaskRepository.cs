using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Domain.Repositories;

namespace Appiume.Web.Modules.TaskCloud.Core.Tasks
{
    /// <summary>
    /// Defines a repository to perform database operations for <see cref="Task"/> Entities.
    /// 
    /// Extends <see cref="IRepository"/> to inherit base repository functionality. 
    /// </summary>
    public interface ITaskRepository : IRepository<Task, long>
    {
        /// <summary>
        /// Gets all tasks with <see cref="Task.AssignedPerson"/> is retrived (Include for EntityFramework, Fetch for NHibernate)
        /// filtered by given conditions.
        /// </summary>
        /// <param name="assignedPersonId">Optional assigned person filter. If it's null, not filtered.</param>
        /// <param name="state">Optional state filter. If it's null, not filtered.</param>
        /// <returns>List of found tasks</returns>
        List<Task> GetAllWithPeople(int? assignedPersonId, TaskState? state);
    }
}
