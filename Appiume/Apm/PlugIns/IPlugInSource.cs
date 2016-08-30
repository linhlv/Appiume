using System;
using System.Collections.Generic;

namespace Appiume.Apm.PlugIns
{
    public interface IPlugInSource
    {
        List<Type> GetModules();
    }
}