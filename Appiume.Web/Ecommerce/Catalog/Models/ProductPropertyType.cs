using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Ecommerce.Catalog.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum ProductPropertyType : int
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,

        /// <summary>
        /// 
        /// </summary>
        TextField = 1,

        /// <summary>
        /// 
        /// </summary>
        MultipleChoiceField = 2,

        /// <summary>
        /// 
        /// </summary>
        CurrencyField = 3,

        /// <summary>
        /// 
        /// </summary>
        DateField = 4,

        /// <summary>
        /// 
        /// </summary>
        HyperLink = 7
    }
}