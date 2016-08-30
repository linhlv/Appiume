namespace Appiume.Apm.MongoDb.Configuration
{
    public interface IApmMongoDbModuleConfiguration
    {
        string ConnectionString { get; set; }

        string DatatabaseName { get; set; }
    }
}
