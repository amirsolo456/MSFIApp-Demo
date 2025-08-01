using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Layouts;

namespace MSFIApp.Services.Common
{
    public class MessageService : IMessageService
    {
        private MessagePopupView _popup;
        private bool _isShowing;

        private ContentPage GetCurrentPage()
        {
            var page = Application.Current?.MainPage;

            // اگر Shell باشه، صفحه فعلی رو پیدا کن
            if (page is Shell shell)
            {
                return shell.CurrentPage as ContentPage;
            }

            // اگر NavigationPage باشه
            if (page is NavigationPage navPage)
            {
                return navPage.CurrentPage as ContentPage;
            }

            // اگر خود ContentPage باشه
            if (page is ContentPage contentPage)
            {
                return contentPage;
            }

            return null;
        }

        private void AddPopupToPage(Page page)
        {
            if (_popup == null)
            {
                _popup = new MessagePopupView();

                if (page is ContentPage contentPage)
                {
                    if (contentPage.Content is Layout<View> layout)
                    {
                        layout.Children.Add(_popup);
                        Microsoft.Maui.Controls.AbsoluteLayout.SetLayoutFlags(_popup, AbsoluteLayoutFlags.All);
                        Microsoft.Maui.Controls.AbsoluteLayout.SetLayoutBounds(_popup, new Rect(0, 0, 1, 0.1));
                    }
                    else
                    {
                        throw new InvalidOperationException("ContentPage.Content is not a Layout<View>");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Page is not a ContentPage");
                }
            }
        }

        public async Task ShowMessageAsync(string message, int duration = 3000, string Context = null)
        {
            if (Context is null)
            {
                if (_isShowing)
                    return;

                _isShowing = true;

                var page = GetCurrentPage();
                if (page == null)
                    throw new InvalidOperationException("No active page found");

                AddPopupToPage(page);

                await _popup.ShowAsync(message, duration);
                _isShowing = false;
            }
            else
            {
                switch (Context)
                {
                    case "Token":
                        await AppShell.Current.GoToAsync("///LoginPage");
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public interface IMessageService
    {
        Task ShowMessageAsync(string message, int duration = 3000, string Context = null);
    }
}


