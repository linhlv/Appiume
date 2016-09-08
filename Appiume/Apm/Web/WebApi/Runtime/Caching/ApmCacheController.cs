using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Extensions;
using Appiume.Apm.Runtime.Caching;
using Appiume.Apm.UI;
using Appiume.Apm.Web.Models;
using Appiume.Apm.Web.WebApi.Controllers;

namespace Appiume.Apm.Web.WebApi.Runtime.Caching
{
    [DontWrapResult]
    public class ApmCacheController : ApmApiController
    {
        private readonly ICacheManager _cacheManager;

        public ApmCacheController(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        [HttpPost]
        [Route("api/ApmCache/Clear")]
        public async Task<AjaxResponse> Clear(ClearCacheModel model)
        {
            if (model.Password.IsNullOrEmpty())
            {
                throw new UserFriendlyException("Password can not be null or empty!");
            }

            if (model.Caches.IsNullOrEmpty())
            {
                throw new UserFriendlyException("Caches can not be null or empty!");
            }

            await CheckPassword(model.Password);

            var caches = _cacheManager.GetAllCaches().Where(c => model.Caches.Contains(c.Name));
            foreach (var cache in caches)
            {
                await cache.ClearAsync();
            }

            return new AjaxResponse();
        }

        [HttpPost]
        [Route("api/ApmCache/ClearAll")]
        public async Task<AjaxResponse> ClearAll(ClearAllCacheModel model)
        {
            if (model.Password.IsNullOrEmpty())
            {
                throw new UserFriendlyException("Password can not be null or empty!");
            }

            await CheckPassword(model.Password);

            var caches = _cacheManager.GetAllCaches();
            foreach (var cache in caches)
            {
                await cache.ClearAsync();
            }

            return new AjaxResponse();
        }

        private async Task CheckPassword(string password)
        {
            var actualPassword = await SettingManager.GetSettingValueAsync(ClearCacheSettingNames.Password);
            if (actualPassword != password)
            {
                throw new UserFriendlyException("Password is not correct!");
            }
        }
    }
}
