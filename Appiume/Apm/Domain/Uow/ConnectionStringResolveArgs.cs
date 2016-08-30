using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.MultiTenancy;

namespace Appiume.Apm.Domain.Uow
{
    public class ConnectionStringResolveArgs : Dictionary<string, object>
    {
        public MultiTenancySides? MultiTenancySide { get; set; }

        public ConnectionStringResolveArgs(MultiTenancySides? multiTenancySide = null)
        {
            MultiTenancySide = multiTenancySide;
        }
    }
}