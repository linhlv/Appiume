using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Web.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMultiTenancyObject
    {
        /// <summary>
        /// 
        /// </summary>
        long MerchantId { get; set; }
    }
}
