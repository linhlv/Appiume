﻿using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Ef;
using Appiume.Apm.Ef.Repositories;

namespace Appiume.Web.Dewey.EntityFramework.Repositories
{
    public abstract class EventCloudRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<DeweyDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected EventCloudRepositoryBase(IDbContextProvider<DeweyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class EventCloudRepositoryBase<TEntity> : EventCloudRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected EventCloudRepositoryBase(IDbContextProvider<DeweyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
