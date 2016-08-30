using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Dependency;

namespace Appiume.Apm.Authorization
{
    internal class PermissionDependencyContext : IPermissionDependencyContext, ITransientDependency
    {
        public UserIdentifier User { get; set; }

        public IIocResolver IocResolver { get; private set; }

        public IPermissionChecker PermissionChecker { get; set; }

        public PermissionDependencyContext(IIocResolver iocResolver)
        {
            IocResolver = iocResolver;
            PermissionChecker = NullPermissionChecker.Instance;
        }
    }
}