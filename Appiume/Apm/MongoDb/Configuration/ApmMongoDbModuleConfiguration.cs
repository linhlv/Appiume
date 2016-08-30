namespace Appiume.Apm.MongoDb.Configuration
{
    internal class ApmMongoDbModuleConfiguration : IApmMongoDbModuleConfiguration
    {
        public string ConnectionString { get; set; }

        public string DatatabaseName { get; set; }
    }
}