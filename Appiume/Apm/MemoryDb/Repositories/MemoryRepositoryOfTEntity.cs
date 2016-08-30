using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Domain.Repositories;

namespace Appiume.Apm.MemoryDb.Repositories
{
    public class MemoryRepository<TEntity> : MemoryRepository<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public MemoryRepository(IMemoryDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }
}
