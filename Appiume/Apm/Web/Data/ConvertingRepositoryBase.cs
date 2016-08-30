using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Logging;

namespace Appiume.Apm.Web.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public abstract class ConvertingRepositoryBase<T, V>
        where T : class, new()
        where V : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        protected ILogger logger;

        /// <summary>
        /// 
        /// </summary>
        protected IRepositoryStrategy<T> repository
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        protected ConvertingRepositoryBase()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool AutoSubmit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int CountOfAll()
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Create(V item)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual List<V> FindAllPaged(int pageNumber, int pageSize)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool SubmitChanges()
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        protected abstract void CopyDataToModel(T data, V model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        protected abstract void CopyModelToData(T data, V model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual bool Delete(Guid key)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        protected virtual void DeleteAllSubItems(V model)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual V Find(Guid key)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        protected virtual V FirstPoco(IQueryable<T> items)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        protected virtual void GetSubItems(V model)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        protected virtual List<V> ListPoco(IQueryable<T> items)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        protected virtual void MergeSubItems(V model)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        protected virtual IQueryable<T> PageItems(int pageNumber, int pageSize, IQueryable<T> items)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        protected virtual V SinglePoco(IQueryable<T> items)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual bool Update(V m, Guid key)
        {
            return false;
        }
    }
}