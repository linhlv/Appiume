namespace Appiume.Apm.Ef
{
    public interface IDbContextResolver
    {
        TDbContext Resolve<TDbContext>(string connectionString);
    }
}