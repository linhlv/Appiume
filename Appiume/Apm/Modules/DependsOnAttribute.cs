using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Apm.Modules
{
    /// <summary>
    /// Used to define dependencies of an Apm module to other modules.
    /// It should be used for a class derived from <see cref="ApmModule"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// Types of depended modules.
        /// </summary>
        public Type[] DependedModuleTypes { get; private set; }

        /// <summary>
        /// Used to define dependencies of an Apm module to other modules.
        /// </summary>
        /// <param name="dependedModuleTypes">Types of depended modules</param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}