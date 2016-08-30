namespace Appiume.Apm.Web.WebApi.ProxyScripting
{
    public interface IApiProxyScriptManager
    {
        string GetScript(ApiProxyGenerationOptions options);
    }
}