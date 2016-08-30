using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum CategorySortOrder : int
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,

        /// <summary>
        /// 
        /// </summary>
        ManualOrder = 1,

        /// <summary>
        /// 
        /// </summary>
        ProductName = 2,

        /// <summary>
        /// 
        /// </summary>
        ProductPriceAscending = 3,

        /// <summary>
        /// 
        /// </summary>
        ProductPriceDescending = 4,

        /// <summary>
        /// 
        /// </summary>
        ManufacturerName = 5
    }

}