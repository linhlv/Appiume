using System;
using System.Data.Entity;
using Appiume.Apm.Domain.Entities;
using Appiume.Apm.Domain.Repositories;

namespace Appiume.Apm.Ef.Repositories
{
    public static class EfRepositoryExtensions
    {
        public static DbContext GetDbContext<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            var repositoryWithDbContext = repository as IRepositoryWithDbContext;
            if (repositoryWithDbContext == null)
            {
                throw new ArgumentException("Given repository does not implement IRepositoryWithDbContext", "repository");
            }

            return repositoryWithDbContext.GetDbContext();
        }
    }
}