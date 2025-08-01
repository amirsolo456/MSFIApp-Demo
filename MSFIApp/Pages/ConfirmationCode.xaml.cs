using MSFIApp.Services.Common;
using MSFIApp.Services.User.PasswordRecovery;
using MSFIApp.ViewModels.User.ConfirmationCode;
using MSFIApp.ViewModels.User.PasswordRecovery;
using System.ComponentModel;
using System.Text;
using MSFIApp.Dtos.User.PasswordRecovery;
#if ANDROID
using Android.Views;
#endif

namespace MSFIApp.Pages;
[QueryProperty(nameof(UserPhone), "UserPhone")]
[QueryProperty(nameof(UserId), "UserId")]
public partial class ConfirmationCode : ContentPage, IQueryAttributable, INotifyPropertyChanged, IOnPageKeyDown
{
    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private readonly ConfirmationCodeViewModel _ConfirmationCodeViewModel;

    public ConfirmationCode(ConfirmationCodeViewModel confirmationCodeViewModel)
    {
        try
        {
            InitializeComponent();
           // BindingContext = this;
            _ConfirmationCodeViewModel = confirmationCodeViewModel;
            LabelVisible = false;
        }
        catch (Exception ex)
        {

        }
    }

    private string _userPhonnumber="";
    public string UserPhonNumber
    {
        get => _userPhonnumber;
        set
        {
            if (_userPhonnumber != value)
            {
                _userPhonnumber = value;
                OnPropertyChanged(nameof(UserPhonNumber));
            }
        }
    }


    private bool _labelVisible = false;
    public bool LabelVisible
    {
        get => _labelVisible;
        set
        {
            if (_labelVisible != value)
            {
                _labelVisible = value;
                OnPropertyChanged(nameof(LabelVisible));
            }
        }
    }

    private bool _shouldStartTimer;
    public bool ShouldStartTimer
    {
        get => _shouldStartTimer;
        set
        {
                _shouldStartTimer = value;
            LabelVisible = !value;
            OnPropertyChanged(nameof(ShouldStartTimer));
        }
    }

    private string _confCode="";
    public string ConfCode
    {
        get => _confCode;
        set
        {

            _confCode = value;
            OnPropertyChanged(nameof(ConfCode));


        }
    }


    public string UserPhone = "";
    public int UserId = 0;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {
            if (query.TryGetValue("UserPhone", out object Name) && query.TryGetValue("UserId", out object Userid))
            {

                UserPhone = Name?.ToString();
                UserId = Convert.ToInt32(Userid ?? 0);
                if (UserId == 0) await AppShell.Current.GoToAsync("///Login");
                StringBuilder builder = new StringBuilder();
                builder.Append("کد ارسال شده به شماره تلفن ");
                builder.Append(UserPhone);
                builder.Append(" را وارد کنید.");
                UserPhonNumber = builder.ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }

    private async void IconButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            await AppShell.Current.GoToAsync("///Login");
        }
        catch (Exception ex)
        {

        }
    }

    private async void PrimaryButton_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(ConfCode) &&  UserId != 0)
            {
                var result = await _ConfirmationCodeViewModel?.CheckCode(ConfCode, userid: UserId);

                if (result != null && result == true)
                {
                    await AppShell.Current.GoToAsync($"///PasswordMaking?UserId={UserId}");
                }
                else
                {
                    OtpEntry.ShowError("کد وارد شده صحیح نیست");
                    LabelVisible = true;
                }
            }
        }
        catch (Exception ex)
        {


        }
    }

    private async void ChangeNumber(object sender, EventArgs e)
    {
        try
        {
            await AppShell.Current.GoToAsync("///PasswordRecovery");
        }
        catch (Exception ex)
        {


        }
    }


    private async void RequestAgainCode(object sender, EventArgs e)
    {
        try
        {
            await Task.Yield();
            var Service = Microsoft.Maui.Controls.Application.Current?.Handler?.MauiContext?.Services.GetService<IPasswordRecoveryService<Response, List<ResponseData>,Request>>();
            var View = new PasswordRecoveryViewModel(Service);

            var result = await View?.SendRequest(username: UserPhone);
            if (result != null)
            {
                if (result.IsFailure)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await ErrorPopup.ShowAsync(result.Error.Message);
                    });
                }
                else
                {
                    await Task.Delay(2000);
                    OtpEntry.ClearError();
                    ShouldStartTimer = true;
                }
            }
        }
        catch (Exception ex)
        {


        }
    }


#if ANDROID
    public bool OnPageKeyDown(Keycode keyCode, KeyEvent e)
    {

        if (keyCode == Keycode.Del && e.Action == KeyEventActions.Down)
        {
            // فرض: کنتـرلـت توی XAML با x:Name="CodeInput"
            OtpEntry.HandleBackspace();

            return true;
        }

        return false;
    }

#endif

}