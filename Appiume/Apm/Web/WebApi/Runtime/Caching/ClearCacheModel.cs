namespace Appiume.Apm.Web.WebApi.Runtime.Caching
{
    public class ClearCacheModel
    {
        public string Password { get; set; }

        public string[] Caches { get; set; }
    }
}