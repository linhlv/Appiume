using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Application.Navigation;
using Appiume.Apm.Collections;

namespace Appiume.Apm.Configuration.Startup
{
    /// <summary>
    /// Used to configure navigation.
    /// </summary>
    public interface INavigationConfiguration
    {
        /// <summary>
        /// List of navigation providers.
        /// </summary>
        ITypeList<NavigationProvider> Providers { get; }
    }
}
