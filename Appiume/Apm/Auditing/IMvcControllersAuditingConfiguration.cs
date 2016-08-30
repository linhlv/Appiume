using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Auditing
{
    /// <summary>
    /// Defines MVC Controller auditing configurations
    /// </summary>
    public interface IMvcControllersAuditingConfiguration
    {
        /// <summary>
        /// Used to enable/disable auditing for MVC controllers.
        /// Default: true.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Used to enable/disable auditing for child MVC actions.
        /// Default: false.
        /// </summary>
        bool IsEnabledForChildActions { get; set; }
    }
}
