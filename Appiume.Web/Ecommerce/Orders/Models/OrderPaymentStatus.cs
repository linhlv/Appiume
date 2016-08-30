using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Orders.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum OrderPaymentStatus : int
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 
        /// </summary>
        Unpaid = 1,

        /// <summary>
        /// 
        /// </summary>
        PartiallyPaid = 2,

        /// <summary>
        /// 
        /// </summary>
        Paid = 3,

        /// <summary>
        /// 
        /// </summary>
        Overpaid = 4
    }
}