using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Application.Navigation
{
    /// <summary>
    /// Provides infrastructure to set navigation.
    /// </summary>
    public interface INavigationProviderContext
    {
        /// <summary>
        /// Gets a reference to the menu manager.
        /// </summary>
        INavigationManager Manager { get; }
    }
}
