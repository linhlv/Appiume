using System;
using System.Threading.Tasks;
using Appiume.Apm.Application.Services;
using Appiume.Apm.IdentityFramework;
using Appiume.Apm.Runtime.Session;
using Appiume.Web.Dewey.Core;
using Appiume.Web.Dewey.Core.MultiTenancy;
using Appiume.Web.Dewey.Core.Users;
using Microsoft.AspNet.Identity;

namespace Appiume.Web.Dewey.Application
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class EventCloudAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected EventCloudAppServiceBase()
        {
            LocalizationSourceName = EventCloudConsts.LocalizationSourceName;
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(ApmSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(ApmSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}