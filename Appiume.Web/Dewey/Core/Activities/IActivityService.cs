using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Domain.Services;

namespace Appiume.Web.Dewey.Core.Activities
{
    public interface IActivityService : IDomainService
    {
        void AddActivity(Activity activity);
    }
}
