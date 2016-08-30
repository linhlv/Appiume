using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Web.Ecommerce.Core;
using Appiume.Apm.Web.Data;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Variant : IMultiTenancyObject, IMultiStore, IStock, IProductRelated, IAvin
    {
        /// <summary>
        /// 
        /// </summary>
        public string Avin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long MerchantId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Variant()
        {
            this.StoreId = 0;
            this.ProductId = string.Empty;
            this.Sku = string.Empty;
            this.Price = -1;
        }
    }
}