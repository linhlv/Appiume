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
    public class OptionItem : IMultiTenancyObject, IMultiStore, ISortable
    {
        /// <summary>
        /// 
        /// </summary>
        public string Avin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OptionAvin { get; set; }

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
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal PriceAdjustment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal WeightAdjustment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLabel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OptionItem()
        {
            this.Avin = string.Empty;
            this.StoreId = 0;
            this.Name = string.Empty;
            this.PriceAdjustment = 0;
            this.WeightAdjustment = 0;
            this.IsLabel = false;
            this.SortOrder = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OptionItem Clone()
        {
            var result = new OptionItem
            {
                IsLabel = this.IsLabel,
                Name = this.Name,
                PriceAdjustment = this.PriceAdjustment,
                SortOrder = this.SortOrder,
                StoreId = this.StoreId,
                WeightAdjustment = this.WeightAdjustment
            };

            return result;
        }
    }
}