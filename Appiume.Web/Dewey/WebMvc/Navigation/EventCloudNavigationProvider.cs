using Appiume.Apm.Application.Navigation;
using Appiume.Apm.Localization;
using Appiume.Web.Dewey.Core;

namespace Appiume.Web.Dewey.WebMvc.Navigation
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class EventCloudNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        AppPageNames.Events,
                        new LocalizableString("Events", EventCloudConsts.LocalizationSourceName),
                        url: "#/",
                        icon: "fa fa-calendar-check-o"
                        )
                );
        }
    }
}
