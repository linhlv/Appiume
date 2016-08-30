using Appiume.Apm.MemoryDb.Uow;
using Appiume.Apm.Dependency;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.MemoryDb;

namespace Appiume.Apm.MemoryDb.Uow
{
    /// <summary>
    /// Implements <see cref="IMemoryDatabaseProvider"/> that gets database from active unit of work.
    /// </summary>
    public class UnitOfWorkMemoryDatabaseProvider : IMemoryDatabaseProvider, ITransientDependency
    {
        public MemoryDatabase Database { get { return ((MemoryDbUnitOfWork)_currentUnitOfWork.Current).Database; } }

        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWork;

        public UnitOfWorkMemoryDatabaseProvider(ICurrentUnitOfWorkProvider currentUnitOfWork)
        {
            _currentUnitOfWork = currentUnitOfWork;
        }
    }
}