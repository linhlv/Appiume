using Appiume.Apm.Web.Mvc.Views;
using Appiume.Web.Dewey.Core;

namespace Appiume.Web.Views
{
    public abstract class ApmWebViewPageBase : ApmWebViewPageBase<dynamic>
    {

    }

    public abstract class ApmWebViewPageBase<TModel> : ApmWebViewPage<TModel>
    {
        protected ApmWebViewPageBase()
        {
            LocalizationSourceName = EventCloudConsts.LocalizationSourceName;
        }
    }
}