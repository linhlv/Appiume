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
    public class ProductPropertyChoice : IMultiStore, ITrackingObject<string>, IMultiTenancyObject, ISortable
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long PropertyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ChoiceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductPropertyChoice()
        {
            this.Id = 0;
            this.ChoiceName = string.Empty;
            this.MerchantId = 0;
            this.StoreId = 0;
            this.CreatedOnUtc = DateTime.UtcNow;
            this.ModifiedOnUtc = DateTime.UtcNow;
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
        public int SortOrder { get; set; }

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