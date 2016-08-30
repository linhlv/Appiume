using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace Appiume.Apm.Modules
{
    /// <summary>
    /// Used to store all needed information for a module.
    /// </summary>
    public class ApmModuleInfo
    {
        /// <summary>
        /// The assembly which contains the module definition.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Type of the module.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Instance of the module.
        /// </summary>
        public ApmModule Instance { get; }

        /// <summary>
        /// All dependent modules of this module.
        /// </summary>
        public List<ApmModuleInfo> Dependencies { get; }

        /// <summary>
        /// Creates a new ApmModuleInfo object.
        /// </summary>
        public ApmModuleInfo([NotNull] Type type, [NotNull] ApmModule instance)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            Type = type;
            Instance = instance;
            Assembly = Type.Assembly;

            Dependencies = new List<ApmModuleInfo>();
        }

        public override string ToString()
        {
            return Type.AssemblyQualifiedName ??
                   Type.FullName;
        }
    }
}