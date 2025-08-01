    using MSFIApp.Pages;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace MSFIApp.Services.Common
    {
        public static class AppActionsHandler
        {
            public static void HandleAppActions(AppAction appAction)
            {
                App.Current.Dispatcher.Dispatch(async () =>
                {
                    Page? page = appAction.Id switch
                    {
                        "battery_info" => new MSFIApp.Pages.PortalGuid(),
                        "app_info" => new MSFIApp.Pages.PortalGuid(),
                        _ => default(Page)
                    };


                    if (page != null)
                    {
                        var nav = Application.Current.Windows[0].Page.Navigation;
                        await nav.PopToRootAsync();
                        await nav.PushAsync(page);
                    }
                });
            }

        }
    }
