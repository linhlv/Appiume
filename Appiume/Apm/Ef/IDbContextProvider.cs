using System;
using System.Data.Entity;
using Appiume.Apm.MultiTenancy;

namespace Appiume.Apm.Ef
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        [Obsolete("Use GetDbContext() method instead")]
        TDbContext DbContext { get; }

        TDbContext GetDbContext();

        TDbContext GetDbContext(MultiTenancySides? multiTenancySide );
    }
}