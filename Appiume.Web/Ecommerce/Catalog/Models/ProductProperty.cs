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
    public class ProductProperty : IMultiStore, ITrackingObject<string>, IMultiTenancyObject
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

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
        public string PropertyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DisplayOnSite { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DisplayToDropShipper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductPropertyType TypeCode { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string FriendlyTypeName
        {
            get
            {
                string result = "Unknown";
                switch (TypeCode)
                {
                    case ProductPropertyType.CurrencyField:
                        result = "Currency";
                        break;
                    case ProductPropertyType.DateField:
                        result = "Date";
                        break;
                    case ProductPropertyType.MultipleChoiceField:
                        result = "Multiple Choice";
                        break;
                    case ProductPropertyType.TextField:
                        result = "Text Block";
                        break;
                    case ProductPropertyType.HyperLink:
                        result = "Hyperlink";
                        break;
                    default:
                        result = "Unknown";
                        break;
                }
                return result;
            }
            // do nothing
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CultureCode { get; set; }

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