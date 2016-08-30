using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.Web.Data
{
    /// <summary>
    /// T : user id type
    /// </summary>
    public interface ITrackingObject<T>
    {
        /// <summary>
        /// 
        /// </summary>
        DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        T CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime ModifiedOnUtc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        T ModifiedBy { get; set; }
    }
}
