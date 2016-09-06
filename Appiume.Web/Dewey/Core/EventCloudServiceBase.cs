using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm;

namespace Appiume.Web.Dewey.Core
{
    public class IoTServiceBase : ApmServiceBase
    {
        public IoTServiceBase()
        {
            LocalizationSourceName = EventCloudConsts.LocalizationSourceName;
        }
    }
}