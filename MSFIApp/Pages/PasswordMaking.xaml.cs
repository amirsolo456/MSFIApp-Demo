using MSFIApp.Services.User.SetPassword;
using System.Text.RegularExpressions;
using MSFIApp.Dtos.User.SetPassword;
using System.Text;
#if ANDROID
using Android.Views;
#endif

namespace MSFIApp.Pages;
[QueryProperty(nameof(UserId), "UserId")]
public partial class PasswordMaking : ContentPage, IQueryAttributable
{
    public int UserId = 0;
    private ISetPasswordService<Response, ResponseData,Request> _SetPasswordService;
    public PasswordMaking(ISetPasswordService<Response, ResponseData, Request> setPasswordService)
    {
        try
        {
            InitializeComponent();
            _SetPasswordService = setPasswordService;
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    void OnBackClicked(object sender, EventArgs e)
    {
        try
        {
            Navigation?.PopAsync();
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    private async void GetBackClick(object sender, EventArgs e)
    {
        try
        {
            await AppShell.Current.GoToAsync("///Login");
        }
        catch (Exception ex)
        {


        }
   
    }

    private async void PasswordInputChecker_Submit(object sender, string e)
    {
        try
        {
            if (!string.IsNullOrEmpty(e))
            {
                if (UserId != 0)
                {
                    var result = await _SetPasswordService?.SetPassword(new Request()
                    {
                        Id = UserId,
                        Password = e,
                        RePassword = e
                    });

                    if (result != null)
                    {
                        if (result.IsFailure)
                        {
                            this.Dispatcher.Dispatch(async () =>
                            {
                                await ErrorPopup.ShowAsync(result.Error.Message);
                            });
                            //MainThread.BeginInvokeOnMainThread(async () =>
                            //{
                            //    await ErrorPopup.ShowAsync(result.Error.Message);
                            //});
                        }
                        else
                        {
                            await AppShell.Current.GoToAsync("///Login");
                        }
                    }
                    else
                    {
                        this.Dispatcher.Dispatch(async () =>
                        {
                            await ErrorPopup.ShowAsync("خطا در برقراری ارتباط با سرور.");
                        });
                    }
                }

            }
        }
        catch (Exception ex)
        {
            this.Dispatcher.Dispatch(async () =>
            {
                await ErrorPopup.ShowAsync(ex.Message);
            });

        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {
            if (query.TryGetValue("UserId", out object Userid))
            {
                UserId = Convert.ToInt32(Userid ?? 0);
            }
        }
        catch (Exception ex)
        {

        }
    }
}