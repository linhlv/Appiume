using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Web.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISearchEngineFriendlyObject
    {
        /// <summary>
        /// 
        /// </summary>
        string MetaKeywords { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string MetaDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string MetaTitle { get; set; }
    }
}
