﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Appiume.Apm.Dependency;
using Appiume.Apm.Localization.Dictionaries;
using Appiume.Apm.Localization.Dictionaries.Xml;

namespace Appiume.Apm.Localization.Sources.Xml
{
    /// <summary>
    /// XML based localization source.
    /// It uses XML files to read localized strings.
    /// </summary>
    [Obsolete("Directly use DictionaryBasedLocalizationSource with XmlFileLocalizationDictionaryProvider instead of this class")]
    public class XmlLocalizationSource : DictionaryBasedLocalizationSource, ISingletonDependency
    {
        internal static string RootDirectoryOfApplication { get; set; } //TODO: Find a better way of passing root directory

        static XmlLocalizationSource()
        {
            RootDirectoryOfApplication = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Creates an Xml based localization source.
        /// </summary>
        /// <param name="name">Unique Name of the source</param>
        /// <param name="directoryPath">Directory path of the localization XML files</param>
        public XmlLocalizationSource(string name, string directoryPath)
            : base(name, new XmlFileLocalizationDictionaryProvider(directoryPath))
        {

        }
    }
}