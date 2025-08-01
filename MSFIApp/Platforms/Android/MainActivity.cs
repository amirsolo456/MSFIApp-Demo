using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using MSFIApp.Services.Common;

namespace MSFIApp;

[Activity(Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        Window.SetSoftInputMode(SoftInput.AdjustResize); // مهم!
    }

    public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
    {
        var page = Shell.Current?.CurrentPage;

        if (page is IOnPageKeyDown keyDownPage)
        {
            bool handled = keyDownPage.OnPageKeyDown(keyCode, e);
            if (handled)
                return true;
        }
        return base.OnKeyDown(keyCode, e);
    }
}


