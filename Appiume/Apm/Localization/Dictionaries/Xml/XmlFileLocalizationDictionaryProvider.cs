﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Appiume.Apm.Localization.Sources.Xml;

namespace Appiume.Apm.Localization.Dictionaries.Xml
{
    /// <summary>
    /// Provides localization dictionaries from XML files in a directory.
    /// </summary>
    public class XmlFileLocalizationDictionaryProvider : LocalizationDictionaryProviderBase
    {
        private readonly string _directoryPath;

        /// <summary>
        /// Creates a new <see cref="XmlFileLocalizationDictionaryProvider"/>.
        /// </summary>
        /// <param name="directoryPath">Path of the dictionary that contains all related XML files</param>
        public XmlFileLocalizationDictionaryProvider(string directoryPath)
        {
            if (!Path.IsPathRooted(directoryPath))
            {
                directoryPath = Path.Combine(XmlLocalizationSource.RootDirectoryOfApplication, directoryPath);
            }

            _directoryPath = directoryPath;
        }

        public override void Initialize(string sourceName)
        {
            var fileNames = Directory.GetFiles(_directoryPath, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var fileName in fileNames)
            {
                var dictionary = CreateXmlLocalizationDictionary(fileName);
                if (Dictionaries.ContainsKey(dictionary.CultureInfo.Name))
                {
                    throw new ApmInitializationException(sourceName + " source contains more than one dictionary for the culture: " + dictionary.CultureInfo.Name);
                }

                Dictionaries[dictionary.CultureInfo.Name] = dictionary;

                if (fileName.EndsWith(sourceName + ".xml"))
                {
                    if (DefaultDictionary != null)
                    {
                        throw new ApmInitializationException("Only one default localization dictionary can be for source: " + sourceName);
                    }

                    DefaultDictionary = dictionary;
                }
            }
        }

        protected virtual XmlLocalizationDictionary CreateXmlLocalizationDictionary(string fileName)
        {
            return XmlLocalizationDictionary.BuildFomFile(fileName);
        }
    }
}