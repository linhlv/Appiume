using System.Collections.Generic;

namespace Appiume.Apm.PlugIns
{
    public class ApmPlugInManager : IApmPlugInManager
    {
        public PlugInSourceList PlugInSources { get; }

        public ApmPlugInManager()
        {
            PlugInSources = new PlugInSourceList();
        }
    }
}