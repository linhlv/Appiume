using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Application.Navigation;
using Appiume.Apm.Localization;
using Appiume.Web.Dewey.Core;

namespace Appiume.Web.Dewey.WebMvc.Navigation
{
    public class UsersNavigationProvider : NavigationProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "UserList",
                        new LocalizableString("UserList", TaskCloudConsts.LocalizationSourceName),
                        url: "#/users",
                        icon: "fa fa-users"
                        )
                );
        }
    }
}