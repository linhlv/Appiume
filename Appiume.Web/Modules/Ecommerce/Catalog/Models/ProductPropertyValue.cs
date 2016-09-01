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
    public class ProductPropertyValue: IProductRelated, IMultiStore, IMultiTenancyObject
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
        public string ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long PropertyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StringValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long StringValueAsLong()
        {
            long result = -1;
            long.TryParse(this.StringValue, out result);
            return result;
        }
    }
}