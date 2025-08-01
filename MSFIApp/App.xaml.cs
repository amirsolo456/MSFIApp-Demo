using Application = Microsoft.Maui.Controls.Application;

namespace MSFIApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#if ANDROID
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
                handler.PlatformView.BackgroundTintList = null;
                handler.PlatformView.Background = null; // حذف بک‌گراند پیش‌فرض (که شامل خط زیرین است)
            });
#endif
            bool hasInternet = Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

            App.Current?.MainPage?.DisplayAlert("خطا", "اتصال به اینترنت برقرار نیست.", "باشه");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            try
            {
                return new Window(new AppShell());
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}