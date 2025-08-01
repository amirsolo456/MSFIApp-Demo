using MSFIApp.ViewModels.User.PasswordRecovery;
using System.ComponentModel;

namespace MSFIApp.Pages;

public partial class PasswordRecovery : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private readonly PasswordRecoveryViewModel _PasswordRecoveryViewModel;
    public PasswordRecovery(PasswordRecoveryViewModel passwordRecoveryViewModel)
    {
        InitializeComponent();
        _PasswordRecoveryViewModel = passwordRecoveryViewModel;
    }

    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            if (_username != value)
            {
                _username = value;
                OnPropertyChanged(nameof(Username)); // ← این خیلی مهمه
            }
        }
    }

    public static readonly BindableProperty IsButtonBusyProperty =
    BindableProperty.Create(nameof(IsButtonBusy), typeof(bool), typeof(SignUp), false);
    public bool IsButtonBusy
    {
        get => (bool)GetValue(IsButtonBusyProperty);
        set
        {
            SetValue(IsButtonBusyProperty, value);
            OnPropertyChanged(nameof(IsButtonBusy));
        }
    }
    private async void PrimaryButton_OnClick(object sender, EventArgs e)
    {
        if (Username is null)
        {
            MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await ErrorPopup.ShowAsync("لطفا کاربر را مشخص کنید");
            });
        }
        if (Username?.Trim().Length != 11)
        {
            MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await ErrorPopup.ShowAsync("کاربر موجود نیست");
            });
        }
        IsButtonBusy = true;
        try
        {
            var response = await _PasswordRecoveryViewModel?.SendRequest(Username);
            if (response != null)
            {
                if (response.IsFailure)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                      await  ErrorPopup.ShowAsync(response.Error.Message);
                    });                    
                }
                else
                {
                    int userId = response.Id;
                    if (userId != null)
                    {
                        await AppShell.Current.GoToAsync($"///ConfirmationCode?UserPhone={Username}&UserId={userId}");
                    }
                }
            }
        }
        catch (Exception ex)
        {


        }
        finally
        {
            IsButtonBusy = false;
        }


        //await AppShell.Current.GoToAsync($"///ConfirmationCode?Username={Username}");
        //if (await _PasswordRecoveryViewModel?.SendRequest(Username))
        //{
        //    await AppShell.Current.GoToAsync("///PasswordChange?Username={Username}");
        //}
    }

    private async void IconButton_Clicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync("///Login");
    }
}