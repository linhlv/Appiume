using Appiume.Apm.Modules;

namespace Appiume.Apm.Owin
{
    /// <summary>
    /// OWIN integration module for Apm.
    /// </summary>
    [DependsOn(typeof (ApmKernelModule))]
    public class ApmOwinModule : ApmModule
    {
        //nothing to do...
    }
}
