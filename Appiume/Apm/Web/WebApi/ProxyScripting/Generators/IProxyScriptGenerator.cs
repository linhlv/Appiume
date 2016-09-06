using Appiume.Apm.Web.WebApi.Modeling;

namespace Appiume.Apm.Web.WebApi.ProxyScripting.Generators
{
    public interface IProxyScriptGenerator
    {
        string CreateScript(ApplicationApiDescriptionModel model);
    }
}