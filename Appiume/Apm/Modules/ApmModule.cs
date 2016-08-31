﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;
using Appiume.Apm.Modules;
using Castle.Core.Logging;
using Appiume.Apm.Collections.Extensions;

namespace Appiume.Apm.Modules
{
    /// <summary>
    /// This class must be implemented by all module definition classes.
    /// </summary>
    /// <remarks>
    /// A module definition class is generally located in it's own assembly
    /// and implements some action in module events on application startup and shutdown.
    /// It also defines depended modules.
    /// </remarks>
    public abstract class ApmModule
    {
        /// <summary>
        /// Gets a reference to the IOC manager.
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        /// <summary>
        /// Gets a reference to the Apm configuration.
        /// </summary>
        protected internal IApmStartupConfiguration Configuration { get; internal set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public ILogger Logger { get; set; }

        protected ApmModule()
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// This is the first event called on application startup. 
        /// Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// This method is used to register dependencies for this module.
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// This method is called lastly on application startup.
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// This method is called when the application is being shutdown.
        /// </summary>
        public virtual void Shutdown()
        {

        }

        public virtual Assembly[] GetAdditionalAssemblies()
        {
            return new Assembly[0];
        }

        /// <summary>
        /// Checks if given type is an Apm module class.
        /// </summary>
        /// <param name="type">Type to check</param>
        public static bool IsApmModule(Type type)
        {
            return
                type.IsClass &&
                !type.IsAbstract &&
                !type.IsGenericType &&
                typeof(ApmModule).IsAssignableFrom(type);
        }

        /// <summary>
        /// Finds direct depended modules of a module.
        /// </summary>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsApmModule(moduleType))
            {
                throw new ApmInitializationException("This type is not an Apm module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }


        /// <summary>
        /// Finds all depended modules (and their dependencies recursively) for a module.
        /// </summary>
        public static List<Type> FindDependedModuleTypesRecursively(Type moduleType)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesResursively(list, moduleType);
            list.AddIfNotContains(typeof(ApmKernelModule));
            return list;
        }

        private static void AddModuleAndDependenciesResursively(List<Type> modules, Type module)
        {
            if (!IsApmModule(module))
            {
                throw new ApmInitializationException("This type is not an Apm module: " + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }

            modules.Add(module);

            var dependedModules = FindDependedModuleTypes(module);
            foreach (var dependedModule in dependedModules)
            {
                AddModuleAndDependenciesResursively(modules, dependedModule);
            }
        }
    }
}