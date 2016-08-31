using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Appiume.Apm.Dependency;
using Appiume.Apm.Localization.Dictionaries;
using Appiume.Apm.Localization.Dictionaries.Xml;
using Appiume.Apm.Modules;
using Appiume.Apm.Web.WebApi.ProxyScripting.Configuration;
using Appiume.Apm.Web.WebApi.ProxyScripting.Generators.JQuery;
using Appiume.Apm.Web.Configuration;
using Appiume.Apm.Web.Localization;

namespace Appiume.Apm.Web
{
    /// <summary>
    /// This module is used to use Apm in ASP.NET web applications.
    /// </summary>
    [DependsOn(typeof(ApmKernelModule))]
    public class ApmWebCommonModule : ApmModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.Register<IApiProxyScriptingConfiguration, ApiProxyScriptingConfiguration>();
            IocManager.Register<IApmWebModuleConfiguration, ApmWebModuleConfiguration>();

            Configuration.Modules.ApmWeb().ApiProxyScripting.Generators[JQueryProxyScriptGenerator.Name] = typeof(JQueryProxyScriptGenerator);

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    ApmWebLocalizedMessages.SourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), "Appiume.Apm.Web.Localization.ApmWebXmlSource"
                        )));
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}