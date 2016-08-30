using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Orders.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderPackageItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string ProductAvin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long LineItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderPackageItem()
        {
            this.ProductAvin = string.Empty;
            this.LineItemId = 0;
            this.Quantity = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="avin"></param>
        /// <param name="itemId"></param>
        /// <param name="qty"></param>
        public OrderPackageItem(string avin, long itemId, int qty)
        {
            this.ProductAvin = avin;
            this.LineItemId = itemId;
            this.Quantity = qty;
        }
    }
}