using Appiume.Apm.Threading;
using Owin;

namespace Appiume.Apm.Owin
{
    /// <summary>
    /// OWIN extension methods for Apm.
    /// </summary>
    public static class ApmOwinExtensions
    {
        /// <summary>
        /// Uses Apm.
        /// </summary>
        public static void UseApm(this IAppBuilder app)
        {
            ThreadCultureSanitizer.Sanitize();
        }
    }
}