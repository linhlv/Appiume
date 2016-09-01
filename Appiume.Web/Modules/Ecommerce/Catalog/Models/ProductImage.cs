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
    public class ProductImage : IMultiStore, ITrackingObject<string>, IMultiTenancyObject, ISortable
    {
        /// <summary>
        /// 
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AlternateText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SortOrder { get; set; }

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
        public ProductImage()
        {
            this.MerchantId = 0;
            this.StoreId = 0;
            this.CreatedOnUtc= DateTime.UtcNow;
            this.ModifiedOnUtc = DateTime.UtcNow;
            this.FileName = string.Empty;
            this.Caption = string.Empty;
            this.AlternateText = string.Empty;
            this.SortOrder = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ProductImage Clone()
        {
            var result = new ProductImage
            {
                AlternateText = this.AlternateText,
                Caption = this.Caption,
                FileName = this.FileName,
                CreatedOnUtc = this.CreatedOnUtc,
                ModifiedOnUtc = this.ModifiedOnUtc,
                ProductId = this.ProductId,
                SortOrder = this.SortOrder,
                StoreId = this.StoreId
            };

            return result;
        }
    }
}