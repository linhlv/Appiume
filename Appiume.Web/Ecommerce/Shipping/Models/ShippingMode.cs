using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Shipping.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum ShippingMode : int
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,

        /// <summary>
        /// 
        /// </summary>
        ShipFromSite = 1,

        /// <summary>
        /// 
        /// </summary>
		ShipFromVendor = 2,

        /// <summary>
        /// 
        /// </summary>
		ShipFromManufacturer = 3
    }
}