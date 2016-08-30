using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Web.Data;

namespace Appiume.Web.Ecommerce.Orders.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderCoupon : IMultiStore, IMultiTenancyObject, ITrackingObject<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderAvin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderCoupon()
        {
            this.Id = 0;
            this.StoreId = 0;
            this.ModifiedOnUtc = DateTime.UtcNow;
            this.OrderAvin = string.Empty;
            this.CouponCode = string.Empty;
            this.IsUsed = false;
            this.UserId = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public long MerchantId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedOnUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}