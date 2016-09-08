using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Domain.Services;

namespace Appiume.Web.Dewey.Core.Notifications
{
    public interface INotificationService : IDomainService
    {
        void Notify(INotification notification);
    }
}
