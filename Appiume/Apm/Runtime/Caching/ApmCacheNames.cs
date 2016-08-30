using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Runtime.Caching
{
    /// <summary>
    /// Names of standard caches used in APM.
    /// </summary>
    public static class ApmCacheNames
    {
        /// <summary>
        /// Application settings cache: ApmApplicationSettingsCache.
        /// </summary>
        public const string ApplicationSettings = "ApmApplicationSettingsCache";

        /// <summary>
        /// Tenant settings cache: ApmTenantSettingsCache.
        /// </summary>
        public const string TenantSettings = "ApmTenantSettingsCache";

        /// <summary>
        /// User settings cache: ApmUserSettingsCache.
        /// </summary>
        public const string UserSettings = "ApmUserSettingsCache";

        /// <summary>
        /// Localization scripts cache: ApmLocalizationScripts.
        /// </summary>
        public const string LocalizationScripts = "ApmLocalizationScripts";
    }
}