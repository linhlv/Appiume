using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Appiume.Apm.Authorization
{
    public interface IAuthorizationHelper
    {
        Task AuthorizeAsync(IEnumerable<IApmAuthorizeAttribute> authorizeAttributes);

        Task AuthorizeAsync(MethodInfo methodInfo);
    }
}