using System.Collections.Generic;
using Appiume.Apm.AutoMapper;
using Appiume.Web.Modules.EventCloud.Core.MultiTenancy;

namespace Appiume.Web.Modules.EventCloud.WebMvc.Models.Account
{
    public class TenantSelectionViewModel
    {
        public string Action { get; set; }

        public List<TenantInfo> Tenants { get; set; }

        [AutoMapFrom(typeof(Tenant))]
        public class TenantInfo
        {
            public int Id { get; set; }

            public string TenancyName { get; set; }

            public string Name { get; set; }
        }
    }
}