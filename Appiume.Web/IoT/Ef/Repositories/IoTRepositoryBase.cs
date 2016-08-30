using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Ef;
using Appiume.Apm.Ef.Repositories;

namespace Appiume.Web.IoT.Ef.Repositories
{
    public abstract class IoTRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<IoTDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected IoTRepositoryBase(IDbContextProvider<IoTDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class IoTRepositoryBase<TEntity> : IoTRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected IoTRepositoryBase(IDbContextProvider<IoTDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
