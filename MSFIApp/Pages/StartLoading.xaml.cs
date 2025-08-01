

using MSFIApp.Services.Common;

namespace MSFIApp.Pages;

public partial class StartLoading : ContentPage
{
    private readonly ISecureStorageService secureStorageService;
    public StartLoading(ISecureStorageService secureStorage)
    {
        InitializeComponent();
        secureStorageService = secureStorage;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Logo.Opacity = 0;

        await Task.WhenAll(
            Logo.FadeTo(1, 600, Easing.CubicOut)

        );


        await Task.Delay(500);


        await Logo.TranslateTo(0, -200, 600, Easing.CubicInOut);


        Loader.IsVisible = true;
        await Loader.FadeTo(1, 500, Easing.CubicOut);
        await Task.Delay(4000);

        if (await secureStorageService.hasToken())
        {
            //await AppShell.Current.GoToAsync("///Login");
            await AppShell.Current.GoToAsync("///MainPage");
        }
        else
        {
            await AppShell.Current.GoToAsync("///Login");
        }
    }
}