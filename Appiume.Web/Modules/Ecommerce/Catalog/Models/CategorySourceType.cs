using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum CategorySourceType : int
    {
        /// <summary>
        /// 
        /// </summary>
        Manual = 0,

        /// <summary>
        /// 
        /// </summary>
        ByRules = 1,

        /// <summary>
        /// 
        /// </summary>
        CustomLink = 2,

        /// <summary>
        /// 
        /// </summary>
        CustomPage = 3,

        /// <summary>
        /// 
        /// </summary>
        DrillDown = 4,

        /// <summary>
        /// 
        /// </summary>
        FlexPage = 5
    }
}