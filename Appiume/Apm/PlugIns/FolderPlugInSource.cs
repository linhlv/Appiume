using System;
using System.Collections.Generic;
using System.IO;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Modules;
using Appiume.Apm.Reflection;

namespace Appiume.Apm.PlugIns
{
    public class FolderPlugInSource : IPlugInSource
    {
        public string Folder { get; }

        public SearchOption SearchOption { get; set; }

        public FolderPlugInSource(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Folder = folder;
            SearchOption = searchOption;
        }

        public List<Type> GetModules()
        {
            var modules = new List<Type>();

            var assemblies = AssemblyHelper.GetAllAssembliesInFolder(Folder, SearchOption);
            foreach (var assembly in assemblies)
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (ApmModule.IsApmModule(type))
                        {
                            modules.AddIfNotContains(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ApmInitializationException("Could not get module types from assembly: " + assembly.FullName, ex);
                }
            }

            return modules;
        }
    }
}