using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Application.Services;
using Appiume.Web.Modules.EventCloud.Application.Sessions.Dto;

namespace Appiume.Web.Modules.EventCloud.Application.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
