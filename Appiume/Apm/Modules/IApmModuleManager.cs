using System;
using System.Collections.Generic;

namespace Appiume.Apm.Modules
{
    public interface IApmModuleManager
    {
        ApmModuleInfo StartupModule { get; }

        IReadOnlyList<ApmModuleInfo> Modules { get; }

        void Initialize(Type startupModule);

        void StartModules();

        void ShutdownModules();
    }
}