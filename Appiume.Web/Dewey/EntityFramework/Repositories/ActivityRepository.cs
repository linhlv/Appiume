using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Ef;
using Appiume.Web.Dewey.Core.Activities;

namespace Appiume.Web.Dewey.EntityFramework.Repositories
{
    public class ActivityRepository : TaskCloudRepositoryBase<Activity, long>, IActivityRepository
    {
        public ActivityRepository(IDbContextProvider<DeweyDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}