using MSFIApp.Services.Common;
using MSFIApp.ViewModels.Settings.PortalGuid;

namespace MSFIApp.Pages;

public partial class PortalGuid : ContentPage, ILoadingTryAgainService.Partision.CodeBihindeProps
{
    private PortalGuidViewModel viewModel;
    public PortalGuid()
    {
        InitializeComponent();
        viewModel = this.BindingContext as PortalGuidViewModel;

    }


    protected override async void OnAppearing()
    {
        OnTryAgainClick(null, null);
        base.OnAppearing();
    }


    private async void IconButton_Clicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync("///Login");
    }

    public async void OnTryAgainClick(object sender, EventArgs e)
    {
        viewModel?.ChnageTurn(false);
        try
        {
            var response = await viewModel?.GetPortablDataAsync();
            if (response != null)
            {
                if (response.IsFailure)
                {
                    MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await ErrorPopup.ShowAsync(response.Error?.Message);
                    });


                    viewModel?.ChnageTurn(true);
                }
                else
                {
                    viewModel?.BuildUI(response.Entity);
                }
            }
        }
        catch (Exception ex)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await ErrorPopup.ShowAsync("خطا در بارگذاری اطلاعات: " + ex.Message);
            });
            viewModel?.ChnageTurn(true);
        }
    }
}