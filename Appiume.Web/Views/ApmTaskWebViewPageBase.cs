using Appiume.Apm.Web.Mvc.Views;
using Appiume.Web.Dewey.Core;

namespace Appiume.Web.Views
{
    public abstract class ApmTaskWebViewPageBase : ApmWebViewPageBase<dynamic>
    {

    }

    public abstract class ApmTaskWebViewPageBase<TModel> : ApmWebViewPage<TModel>
    {
        protected ApmTaskWebViewPageBase()
        {
            LocalizationSourceName = "TaskCloud";
        }
    }
}