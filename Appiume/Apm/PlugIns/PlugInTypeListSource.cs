using System;
using System.Collections.Generic;
using System.Linq;

namespace Appiume.Apm.PlugIns
{
    public class PlugInTypeListSource : IPlugInSource
    {
        private readonly Type[] _moduleTypes;

        public PlugInTypeListSource(params Type[] moduleTypes)
        {
            _moduleTypes = moduleTypes;
        }

        public List<Type> GetModules()
        {
            return _moduleTypes.ToList();
        }
    }
}