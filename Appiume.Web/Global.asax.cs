using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Appiume.Apm.Dependency;
using Appiume.Apm.Web;
using Castle.Facilities.Logging;

namespace Appiume.Web
{
    public class MvcApplication : ApmWebApplication<IoTWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));
            base.Application_Start(sender, e);
        }
    }
}