using System.Data.Entity;

namespace Appiume.Apm.Ef.Repositories
{
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}