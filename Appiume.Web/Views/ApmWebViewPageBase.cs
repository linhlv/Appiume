using Appiume.Apm.Web.Mvc.Views;
using Appiume.Web.IoT.Core;

namespace Appiume.Web.Views
{
    public abstract class ApmWebViewPageBase : ApmWebViewPageBase<dynamic>
    {

    }

    public abstract class ApmWebViewPageBase<TModel> : ApmWebViewPage<TModel>
    {
        protected ApmWebViewPageBase()
        {
            LocalizationSourceName = IoTConsts.LocalizationSourceName;
        }
    }
}