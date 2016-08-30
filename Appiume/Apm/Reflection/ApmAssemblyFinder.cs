using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Appiume.Apm.Modules;

namespace Appiume.Apm.Reflection
{
    public class ApmAssemblyFinder : IAssemblyFinder
    {
        private readonly IApmModuleManager _moduleManager;

        public ApmAssemblyFinder(IApmModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public List<Assembly> GetAllAssemblies()
        {
            var assemblies = new List<Assembly>();

            foreach (var module in _moduleManager.Modules)
            {
                assemblies.Add(module.Assembly);
                assemblies.AddRange(module.Instance.GetAdditionalAssemblies());
            }

            return assemblies.Distinct().ToList();
        }
    }
}