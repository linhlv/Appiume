using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using Appiume.Apm.Application.Features;
using Appiume.Apm.Dependency;
using Appiume.Apm.Localization;
using Appiume.Apm.Reflection;
using Appiume.Apm.Runtime.Session;

namespace Appiume.Apm.Authorization
{
    internal class AuthorizationHelper : IAuthorizationHelper, ITransientDependency
    {
        public IApmSession ApmSession { get; set; }
        public IPermissionChecker PermissionChecker { get; set; }
        public IFeatureChecker FeatureChecker { get; set; }
        public ILocalizationManager LocalizationManager { get; set; }

        private readonly IFeatureChecker _featureChecker;

        public AuthorizationHelper(IFeatureChecker featureChecker)
        {
            _featureChecker = featureChecker;
            ApmSession = NullApmSession.Instance;
            PermissionChecker = NullPermissionChecker.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        public async Task AuthorizeAsync(IEnumerable<IApmAuthorizeAttribute> authorizeAttributes)
        {
            if (!ApmSession.UserId.HasValue)
            {
                throw new ApmAuthorizationException(LocalizationManager.GetString(ApmConsts.LocalizationSourceName, "CurrentUserDidNotLoginToTheApplication"));
            }

            foreach (var authorizeAttribute in authorizeAttributes)
            {
                await PermissionChecker.AuthorizeAsync(authorizeAttribute.RequireAllPermissions, authorizeAttribute.Permissions);
            }
        }

        public async Task AuthorizeAsync(MethodInfo methodInfo)
        {
            if (AllowAnonymous(methodInfo))
            {
                return;
            }
            
            //Authorize
            await CheckFeatures(methodInfo);
            await CheckPermissions(methodInfo);
        }

        private async Task CheckFeatures(MethodInfo methodInfo)
        {
            var featureAttributes =
                ReflectionHelper.GetAttributesOfMemberAndDeclaringType<RequiresFeatureAttribute>(
                    methodInfo
                    );

            if (featureAttributes.Count <= 0)
            {
                return;
            }

            foreach (var featureAttribute in featureAttributes)
            {
                await _featureChecker.CheckEnabledAsync(featureAttribute.RequiresAll, featureAttribute.Features);
            }
        }

        private async Task CheckPermissions(MethodInfo methodInfo)
        {
            var authorizeAttributes =
                ReflectionHelper.GetAttributesOfMemberAndDeclaringType(
                    methodInfo
                ).OfType<IApmAuthorizeAttribute>().ToArray();

            if (!authorizeAttributes.Any())
            {
                return;
            }

            await AuthorizeAsync(authorizeAttributes);
        }

        private static bool AllowAnonymous(MethodInfo methodInfo)
        {
            return ReflectionHelper.GetAttributesOfMemberAndDeclaringType(methodInfo)
                .OfType<IApmAllowAnonymousAttribute>().Any();
        }
    }
}