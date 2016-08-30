using System.Threading.Tasks;
using Appiume.Apm.Dependency;
using Appiume.Apm.Domain.Uow;
using Appiume.Apm.MongoDb.Configuration;
using MongoDB.Driver;

namespace Appiume.Apm.MongoDb.Uow
{
    /// <summary>
    /// Implements Unit of work for MongoDB.
    /// </summary>
    public class MongoDbUnitOfWork : UnitOfWorkBase, ITransientDependency
    {
        /// <summary>
        /// Gets a reference to MongoDB Database.
        /// </summary>
        public MongoDatabase Database { get; private set; }

        private readonly IApmMongoDbModuleConfiguration _configuration;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MongoDbUnitOfWork(IApmMongoDbModuleConfiguration configuration, IConnectionStringResolver connectionStringResolver, IUnitOfWorkDefaultOptions defaultOptions)
            : base(connectionStringResolver, defaultOptions)
        {
            _configuration = configuration;
        }

        protected override void BeginUow()
        {
            Database = new MongoClient(_configuration.ConnectionString)
                .GetServer()
                .GetDatabase(_configuration.DatatabaseName);
        }

        public override void SaveChanges()
        {

        }

        public override async Task SaveChangesAsync()
        {

        }

        protected override void CompleteUow()
        {

        }

        protected override async Task CompleteUowAsync()
        {

        }

        protected override void DisposeUow()
        {

        }
    }
}