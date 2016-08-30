using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Collections;

namespace Appiume.Apm.Configuration.Startup
{
    /// <summary>
    /// Used to configure setting system.
    /// </summary>
    public interface ISettingsConfiguration
    {
        /// <summary>
        /// List of settings providers.
        /// </summary>
        ITypeList<SettingProvider> Providers { get; }
    }
}