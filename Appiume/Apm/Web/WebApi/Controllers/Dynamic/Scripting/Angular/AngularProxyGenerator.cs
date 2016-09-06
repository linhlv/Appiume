using System.Text;

namespace Appiume.Apm.Web.WebApi.Controllers.Dynamic.Scripting.Angular
{
    internal class AngularProxyGenerator : IScriptProxyGenerator
    {
        private readonly DynamicApiControllerInfo _controllerInfo;

        public AngularProxyGenerator(DynamicApiControllerInfo controllerInfo)
        {
            _controllerInfo = controllerInfo;
        }

        public string Generate()
        {
            var script = new StringBuilder();

            script.AppendLine("(function (apm, angular) {");
            script.AppendLine("");
            script.AppendLine("    if (!angular) {");
            script.AppendLine("        return;");
            script.AppendLine("    }");
            script.AppendLine("    ");
            script.AppendLine("    var apmModule = angular.module('apm');");
            script.AppendLine("    ");
            script.AppendLine("    apmModule.factory('apm.services." + _controllerInfo.ServiceName.Replace("/", ".") + "', [");
            script.AppendLine("        '$http', function ($http) {");
            script.AppendLine("            return new function () {");

            foreach (var methodInfo in _controllerInfo.Actions.Values)
            {
                var actionWriter = new AngularActionScriptWriter(_controllerInfo, methodInfo);
                actionWriter.WriteTo(script);
            }

            script.AppendLine("            };");
            script.AppendLine("        }");
            script.AppendLine("    ]);");
            script.AppendLine();

            script.AppendLine();
            script.AppendLine("})((apm || (apm = {})), (angular || undefined));");

            return script.ToString();
        }
    }
}