using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;
using Appiume.Apm.PlugIns;
using Appiume.Apm.Modules;
using Appiume.Apm;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;
using Appiume.Apm.Modules;
using Castle.Core.Logging;

namespace Appiume.Apm.Modules
{
    /// <summary>
    /// This class is used to manage modules.
    /// </summary>
    public class ApmModuleManager : IApmModuleManager
    {
        public ApmModuleInfo StartupModule { get; private set; }

        private Type _startupModuleType;

        public IReadOnlyList<ApmModuleInfo> Modules => _modules.ToImmutableList();

        public ILogger Logger { get; set; }

        private readonly IIocManager _iocManager;
        private readonly IApmPlugInManager _apmPlugInManager;
        private readonly ApmModuleCollection _modules;

        public ApmModuleManager(IIocManager iocManager, IApmPlugInManager apmPlugInManager)
        {
            _modules = new ApmModuleCollection();
            _iocManager = iocManager;
            _apmPlugInManager = apmPlugInManager;
            Logger = NullLogger.Instance;
        }

        public virtual void Initialize(Type startupModule)
        {
            _startupModuleType = startupModule;
            LoadAllModules();
        }

        public virtual void StartModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        public virtual void ShutdownModules()
        {
            Logger.Debug("Shutting down has been started");

            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());

            Logger.Debug("Shutting down completed.");
        }

        private void LoadAllModules()
        {
            Logger.Debug("Loading Apm modules...");

            var moduleTypes = FindAllModules();

            Logger.Debug("Found " + moduleTypes.Count + " Apm modules in total.");

            RegisterModules(moduleTypes);
            CreateModules(moduleTypes);

            ApmModuleCollection.EnsureKernelModuleToBeFirst(_modules);

            SetDependencies();

            Logger.DebugFormat("{0} modules loaded.", _modules.Count);
        }

        private List<Type> FindAllModules()
        {
            var modules = ApmModule.FindDependedModuleTypesRecursively(_startupModuleType);
            AddPlugInModules(modules);
            return modules;
        }

        private void AddPlugInModules(List<Type> modules)
        {
            foreach (var plugInSource in _apmPlugInManager.PlugInSources)
            {
                foreach (var module in plugInSource.GetModules())
                {
                    modules.AddIfNotContains(module);
                }
            }
        }

        private void CreateModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = _iocManager.Resolve(moduleType) as ApmModule;
                if (moduleObject == null)
                {
                    throw new ApmInitializationException("This type is not an Apm module: " + moduleType.AssemblyQualifiedName);
                }

                moduleObject.IocManager = _iocManager;
                moduleObject.Configuration = _iocManager.Resolve<IApmStartupConfiguration>();

                var moduleInfo = new ApmModuleInfo(moduleType, moduleObject);

                _modules.Add(moduleInfo);

                if (moduleType == _startupModuleType)
                {
                    StartupModule = moduleInfo;
                }

                Logger.DebugFormat("Loaded module: " + moduleType.AssemblyQualifiedName);
            }
        }

        private void RegisterModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                _iocManager.RegisterIfNot(moduleType);
            }
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                moduleInfo.Dependencies.Clear();

                //Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in ApmModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new ApmInitializationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }
    }
}
