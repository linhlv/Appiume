using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Application.Navigation;
using Appiume.Apm.Localization;
using Appiume.Web.Dewey.Core;

namespace Appiume.Web.Dewey.WebMvc.Navigation
{
    public class TaskCloudNavigationProvider : NavigationProvider
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
                        "TaskList",
                        new LocalizableString("TaskList", TaskCloudConsts.LocalizationSourceName),
                        url: "#/tasks",
                        icon: "fa fa-tasks"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "NewTask",
                        new LocalizableString("NewTask", TaskCloudConsts.LocalizationSourceName),
                        url: "#/tasks/new",
                        icon: "fa fa-asterisk"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        AppPageNames.About,
                        new LocalizableString("About", EventCloudConsts.LocalizationSourceName),
                        url: "#/about",
                        icon: "fa fa-info"
                        )
                );
        }
    }
}