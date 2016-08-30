using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Logging;

namespace Appiume.Apm.Web.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryStrategy<T> where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        bool AutoSubmit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object ObjectContext { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int CountOfAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Create(T item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Find();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindByPrimaryKey(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool SubmitChanges();
    }
}
