using Appiume.Apm.Domain.Uow;
using Appiume.Apm.Ef.Common;

namespace Appiume.Apm.Ef
{
    public class DbContextTypeMatcher : DbContextTypeMatcher<ApmDbContext>
    {
        public DbContextTypeMatcher(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) 
            : base(currentUnitOfWorkProvider)
        {
        }
    }
}