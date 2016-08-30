using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum ProductInventoryMode
    {
        /// <summary>
        /// 
        /// </summary>
        NotSet = -1,

        /// <summary>
        /// 
        /// </summary>
        AlwayInStock = 100,

        /// <summary>
        /// 
        /// </summary>
        WhenOutOfStockHide = 101,

        /// <summary>
        /// 
        /// </summary>
        WhenOutOfStockShow = 102,

        /// <summary>
        /// 
        /// </summary>
        WhenOutOfStockAllowBackorders = 103
    }
}