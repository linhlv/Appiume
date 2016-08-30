using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Dependency;

namespace Appiume.Apm.Application.Navigation
{
    /// <summary>
    /// This interface should be implemented by classes which change
    /// navigation of the application.
    /// </summary>
    public abstract class NavigationProvider : ITransientDependency
    {
        /// <summary>
        /// Used to set navigation.
        /// </summary>
        /// <param name="context">Navigation context</param>
        public abstract void SetNavigation(INavigationProviderContext context);
    }
}