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
    public class OrderNote : IMultiStore, IMultiTenancyObject, ITrackingObject<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

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
        public string OrderID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AuditDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderNote()
        {
            this.Id = 0;
            this.StoreId = 0;
            this.CreatedOnUtc = DateTime.UtcNow;
            this.ModifiedOnUtc = DateTime.UtcNow;
            this.OrderID = string.Empty;
            this.AuditDate = DateTime.UtcNow;
            this.Note = string.Empty;
            this.IsPublic = false;
        }

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