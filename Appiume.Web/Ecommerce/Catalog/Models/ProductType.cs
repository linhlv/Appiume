using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Web.Data;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductType: IMultiTenancyObject, IMultiStore, ITrackingObject<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsPermanent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductTypeName { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public ProductType()
        {
            this.MerchantId = 0;
            this.StoreId = 0;
            this.CreatedOnUtc = DateTime.UtcNow;
            this.ModifiedOnUtc = DateTime.UtcNow;
            this.ProductTypeName = string.Empty;
            this.IsPermanent = false;
        }
    }
}