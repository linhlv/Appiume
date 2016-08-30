﻿using System.Threading.Tasks;

namespace Appiume.Apm.Web.Navigation
{
    /// <summary>
    /// Used to generate navigation scripts.
    /// </summary>
    public interface INavigationScriptManager
    {
        /// <summary>
        /// Used to generate navigation scripts.
        /// </summary>
        /// <returns></returns>
        Task<string> GetScriptAsync();
    }
}
