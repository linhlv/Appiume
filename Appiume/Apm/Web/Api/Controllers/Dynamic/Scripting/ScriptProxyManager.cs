﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Dependency;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic.Scripting.Angular;
using Appiume.Apm.Web.WebApi.Controllers.Dynamic.Scripting.jQuery;

namespace Appiume.Apm.Web.WebApi.Controllers.Dynamic.Scripting
{
    //TODO@Halil: This class can be optimized.
    public class ScriptProxyManager : ISingletonDependency
    {
        private readonly IDictionary<string, ScriptInfo> CachedScripts;

        public ScriptProxyManager()
        {
            CachedScripts = new Dictionary<string, ScriptInfo>();
        }

        public string GetScript(string name, ProxyScriptType type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name is null or empty!", "name");
            }

            var cacheKey = type + "_" + name;

            lock (CachedScripts)
            {
                var cachedScript = CachedScripts.GetOrDefault(cacheKey);
                if (cachedScript == null)
                {
                    var dynamicController = DynamicApiControllerManager.GetAll().FirstOrDefault(ci => ci.ServiceName == name);
                    if (dynamicController == null)
                    {
                        throw new HttpException(404, "There is no such a service: " + cacheKey);
                    }

                    var script = CreateProxyGenerator(type, dynamicController, true).Generate();
                    CachedScripts[cacheKey] = cachedScript = new ScriptInfo(script);
                }

                return cachedScript.Script;
            }
        }

        public string GetAllScript(ProxyScriptType type)
        {
            lock (CachedScripts)
            {
                var cacheKey = type + "_all";
                if (!CachedScripts.ContainsKey(cacheKey))
                {
                    var script = new StringBuilder();

                    var dynamicControllers = DynamicApiControllerManager.GetAll();
                    foreach (var dynamicController in dynamicControllers)
                    {
                        var proxyGenerator = CreateProxyGenerator(type, dynamicController, false);
                        script.AppendLine(proxyGenerator.Generate());
                        script.AppendLine();
                    }

                    CachedScripts[cacheKey] = new ScriptInfo(script.ToString());
                }

                return CachedScripts[cacheKey].Script;
            }
        }

        private static IScriptProxyGenerator CreateProxyGenerator(ProxyScriptType type, DynamicApiControllerInfo controllerInfo, bool amdModule)
        {
            switch (type)
            {
                case ProxyScriptType.JQuery:
                    return new JQueryProxyGenerator(controllerInfo, amdModule);
                case ProxyScriptType.Angular:
                    return new AngularProxyGenerator(controllerInfo);
                default:
                    throw new ApmException("Unknown ProxyScriptType: " + type);
            }
        }

        private class ScriptInfo
        {
            public string Script { get; private set; }

            public ScriptInfo(string script)
            {
                Script = script;
            }
        }
    }
}