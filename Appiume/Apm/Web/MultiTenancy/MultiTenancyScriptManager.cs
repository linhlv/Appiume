using System;
using System.Globalization;
using System.Text;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;
using Appiume.Apm.Extensions;
using Appiume.Apm.MultiTenancy;

namespace Appiume.Apm.Web.MultiTenancy
{
    public class MultiTenancyScriptManager : IMultiTenancyScriptManager, ITransientDependency
    {
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        public MultiTenancyScriptManager(IMultiTenancyConfig multiTenancyConfig)
        {
            _multiTenancyConfig = multiTenancyConfig;
        }

        public string GetScript()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(apm){");
            script.AppendLine();

            script.AppendLine("    apm.multiTenancy = apm.multiTenancy || {};");
            script.AppendLine("    apm.multiTenancy.isEnabled = " + _multiTenancyConfig.IsEnabled.ToString().ToLower(CultureInfo.InvariantCulture) + ";");
            
            var sideNames = Enum.GetNames(typeof (MultiTenancySides));

            script.AppendLine("    apm.multiTenancy.sides = {");
            for (int i = 0; i < sideNames.Length; i++)
            {
                var sideName = sideNames[i];
                script.Append("        " + sideName.ToCamelCase() + ": " + ((int) sideName.ToEnum<MultiTenancySides>()));
                if (i == sideNames.Length - 1)
                {
                    script.AppendLine();
                }
                else
                {
                    script.AppendLine(",");
                }
            }

            script.AppendLine("    };");

            script.AppendLine();
            script.Append("})(apm);");

            return script.ToString();
        }
    }
}