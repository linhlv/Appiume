using System.Threading.Tasks;
using Appiume.Apm.Domain.Services;
using Appiume.Web.IoT.Core.Users;

namespace Appiume.Web.IoT.Core.Events
{
    public interface IEventRegistrationPolicy : IDomainService
    {
        /// <summary>
        /// Checks if given user can register to <see cref="@event"/> and throws exception if can not.
        /// </summary>
        Task CheckRegistrationAttemptAsync(Event @event, User user);
    }
}