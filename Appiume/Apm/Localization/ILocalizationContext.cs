using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Localization
{
    /// <summary>
    /// Localization context.
    /// </summary>
    public interface ILocalizationContext
    {
        /// <summary>
        /// Gets the localization manager.
        /// </summary>
        ILocalizationManager LocalizationManager { get; }
    }
}
