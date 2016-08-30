using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Orders.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum OrderShippingStatus : int
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 
        /// </summary>
        Unshipped = 1,

        /// <summary>
        /// 
        /// </summary>
        PartiallyShipped = 2,

        /// <summary>
        /// 
        /// </summary>
        FullyShipped = 3,

        /// <summary>
        /// 
        /// </summary>
        NonShipping = 4
    }
}