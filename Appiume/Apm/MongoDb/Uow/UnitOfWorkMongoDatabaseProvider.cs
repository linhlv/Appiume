using Appiume.Apm.Dependency;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.MongoDb.Uow;
using MongoDB.Driver;

namespace Appiume.Apm.MongoDb.Uow
{
    /// <summary>
    /// Implements <see cref="IMongoDatabaseProvider"/> that gets database from active unit of work.
    /// </summary>
    public class UnitOfWorkMongoDatabaseProvider : IMongoDatabaseProvider, ITransientDependency
    {
        public MongoDatabase Database { get { return ((MongoDbUnitOfWork)_currentUnitOfWork.Current).Database; } }

        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWork;

        public UnitOfWorkMongoDatabaseProvider(ICurrentUnitOfWorkProvider currentUnitOfWork)
        {
            _currentUnitOfWork = currentUnitOfWork;
        }
    }
}