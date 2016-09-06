using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Modules;
using Appiume.Apm.Web.WebApi.Configuration.Startup;
using Swashbuckle.Application;

namespace Appiume.Web
{
    public class SwaggerWebApiModule : ApmModule
    {
        public override void Initialize()
        {
            //your other code...

            ConfigureSwaggerUi();
        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.ApmWebApi().HttpConfiguration
                .EnableSwagger("docs/{apiVersion}/swagger/", c =>
                 {
                     c.SingleApiVersion("v1", "Appiume.Web");
                     c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                 })
                .EnableSwaggerUi();
        }
    }
}