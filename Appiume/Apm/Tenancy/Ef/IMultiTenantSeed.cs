using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Tenancy.MultiTenancy;

namespace Appiume.Apm.Tenancy.Ef
{
    public interface IMultiTenantSeed
    {
        ApmTenantBase Tenant { get; set; }
    }
}