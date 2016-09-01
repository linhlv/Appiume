using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum ProductStatus : int
    {
        /// <summary>
        /// 
        /// </summary>
        Active = 1,

        /// <summary>
        /// 
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// 
        /// </summary>
        NotSet = -1
    }
}