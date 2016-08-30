using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Dependency;

namespace Appiume.Apm.ObjectMapping
{
    public sealed class NullObjectMapper : IObjectMapper, ISingletonDependency
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullObjectMapper Instance { get { return SingletonInstance; } }
        private static readonly NullObjectMapper SingletonInstance = new NullObjectMapper();

        public TDestination Map<TDestination>(object source)
        {
            throw new ApmException("Apm.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new ApmException("Apm.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }
    }
}