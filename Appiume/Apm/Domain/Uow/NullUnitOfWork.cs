﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Appiume.Apm.Domain.Uow
{
    /// <summary>
    /// Null implementation of unit of work.
    /// It's used if no component registered for <see cref="IUnitOfWork"/>.
    /// This ensures working Apm without a database.
    /// </summary>
    public sealed class NullUnitOfWork : UnitOfWorkBase
    {
        public override void SaveChanges()
        {

        }

        public override Task SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        protected override void BeginUow()
        {

        }

        protected override void CompleteUow()
        {

        }

        protected override Task CompleteUowAsync()
        {
            return Task.FromResult(0);
        }

        protected override void DisposeUow()
        {

        }

        public NullUnitOfWork(
            IConnectionStringResolver connectionStringResolver,
            IUnitOfWorkDefaultOptions defaultOptions)
            : base(
                connectionStringResolver,
                defaultOptions)
        {
        }
    }
}