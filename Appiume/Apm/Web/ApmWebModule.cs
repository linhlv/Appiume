using System.Reflection;
using System.Web;
using Appiume.Apm.Localization.Sources.Xml;
using Appiume.Apm.Modules;

namespace Appiume.Apm.Web
{
    /// <summary>
    /// This module is used to use Apm in ASP.NET web applications.
    /// </summary>
    [DependsOn(typeof(ApmWebCommonModule))]    
    public class ApmWebModule : ApmModule
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            if (HttpContext.Current != null)
            {
                XmlLocalizationSource.RootDirectoryOfApplication = HttpContext.Current.Server.MapPath("~");
            }
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());            
        }
    }
}
