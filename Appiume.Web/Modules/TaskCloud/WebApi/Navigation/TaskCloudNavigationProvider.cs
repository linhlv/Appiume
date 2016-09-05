using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Apm.Application.Navigation;
using Appiume.Apm.Localization;
using Appiume.Web.Modules.TaskCloud.Core;

namespace Appiume.Web.Modules.TaskCloud.WebApi.Navigation
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
                        url: "#/",
                        icon: "fa fa-tasks"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "NewTask",
                        new LocalizableString("NewTask", TaskCloudConsts.LocalizationSourceName),
                        url: "#/new",
                        icon: "fa fa-asterisk"
                        )
                );
        }
    }
}