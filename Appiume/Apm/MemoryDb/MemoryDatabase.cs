using System;
using System.Collections.Generic;
using Appiume.Apm.Dependency;
using Appiume.Apm.Modules;

namespace Appiume.Apm.MemoryDb
{
    [DependsOn(typeof(ApmKernelModule))]
    public class MemoryDatabase : ISingletonDependency
    {
        private readonly Dictionary<Type, object> _sets;

        private readonly object _syncObj = new object();

        public MemoryDatabase()
        {
            _sets = new Dictionary<Type, object>();
        }

        public List<TEntity> Set<TEntity>()
        {
            var entityType = typeof(TEntity);

            lock (_syncObj)
            {
                if (!_sets.ContainsKey(entityType))
                {
                    _sets[entityType] = new List<TEntity>();
                }

                return _sets[entityType] as List<TEntity>;
            }
        }
    }
}