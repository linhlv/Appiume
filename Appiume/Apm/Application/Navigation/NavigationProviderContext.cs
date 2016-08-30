using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Application.Navigation
{
    internal class NavigationProviderContext : INavigationProviderContext
    {
        public INavigationManager Manager { get; private set; }

        public NavigationProviderContext(INavigationManager manager)
        {
            Manager = manager;
        }
    }
}