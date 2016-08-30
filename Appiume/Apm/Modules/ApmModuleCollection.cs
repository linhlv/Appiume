using System.Collections.Generic;
using System.Linq;
using Appiume.Apm.Collections.Extensions;

namespace Appiume.Apm.Modules
{
    /// <summary>
    /// Used to store ApmModuleInfo objects as a dictionary.
    /// </summary>
    internal class ApmModuleCollection : List<ApmModuleInfo>
    {
        /// <summary>
        /// Gets a reference to a module instance.
        /// </summary>
        /// <typeparam name="TModule">Module type</typeparam>
        /// <returns>Reference to the module instance</returns>
        public TModule GetModule<TModule>() where TModule : ApmModule
        {
            var module = this.FirstOrDefault(m => m.Type == typeof(TModule));
            if (module == null)
            {
                throw new ApmException("Can not find module for " + typeof(TModule).FullName);
            }

            return (TModule)module.Instance;
        }

        /// <summary>
        /// Sorts modules according to dependencies.
        /// If module A depends on module B, A comes after B in the returned List.
        /// </summary>
        /// <returns>Sorted list</returns>
        public List<ApmModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureKernelModuleToBeFirst(sortedModules);
            return sortedModules;
        }

        public static void EnsureKernelModuleToBeFirst(List<ApmModuleInfo> modules)
        {
            var kernelModuleIndex = modules.FindIndex(m => m.Type == typeof (ApmKernelModule));
            if (kernelModuleIndex > 0)
            {
                var kernelModule = modules[kernelModuleIndex];
                modules.RemoveAt(kernelModuleIndex);
                modules.Insert(0, kernelModule);
            }
        }
    }
}