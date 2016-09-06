using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Ef;
using Appiume.Apm.Ef.Repositories;

namespace Appiume.Web.Dewey.EntityFramework.Repositories
{
    public class TaskCloudRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<DeweyDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected TaskCloudRepositoryBase(IDbContextProvider<DeweyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    /// <summary>
    /// A shortcut of <see cref="TaskCloudRepositoryBase{TEntity,TPrimaryKey}"/> for Entities with primary key type <see cref="int"/>.
    /// </summary>
    public abstract class TaskCloudRepositoryBase<TEntity> : TaskCloudRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected TaskCloudRepositoryBase(IDbContextProvider<DeweyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}