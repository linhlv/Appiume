using System.Threading.Tasks;

namespace Appiume.Apm.Web.Timing
{
    /// <summary>
    /// Define interface to get timing scripts
    /// </summary>
    public interface ITimingScriptManager
    {
        /// <summary>
        /// Gets Javascript that contains all feature information.
        /// </summary>
        Task<string> GetScriptAsync();
    }
}
