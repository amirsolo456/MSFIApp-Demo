using MSFIApp.Dtos.Autorize.Login;
using MSFIApp.Services.Autorize;
using MSFIApp.Services.Common;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;

namespace MSFIApp.Pages;

public partial class Login : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private readonly ILoginService<Response, ResponseData, Request> _loginService;
    private readonly ISecureStorageService _secureStorageService;
    public Login(ILoginService<Response, ResponseData, Request> loginService, ISecureStorageService secureStorageService)
    {
        try
        {
            InitializeComponent();
            _loginService = loginService;
            _secureStorageService = secureStorageService;
            BindingContext = this;
        }
        catch (Exception ex)
        {

        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            //await Task.Run(() =>
            //{
            //    var user = _secureStorageService?.GetUser();
            //    if (user != null && user.Result is MSFIApp.Dtos.Autorize.Login.ResponseData userdt)
            //    {
            //        MainThread.InvokeOnMainThreadAsync(() =>
            //        {
            //            userName = userdt.UserName ?? "";
            //            Password = userdt.Password ?? "";
            //        });
            //    }
            //});
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    //private bool _rememberMe;
    //public bool RememberMe
    //{
    //    get => _rememberMe;
    //    set
    //    {
    //        if (_rememberMe != value)
    //        {
    //            _rememberMe = value;
    //            OnPropertyChanged(nameof(RememberMe));
    //        }
    //    }
    //}

    private string _username;
    public string userName
    {
        get => _username;
        set
        {
            if (_username != value)
            {
                _username = value;
                OnPropertyChanged(nameof(userName));
                CheckButtonEnable();
            }
        }
    }

    private bool _buttoneisible = false;
    public bool Buttonenable
    {
        get => _buttoneisible;
        set
        {
            if (_buttoneisible != value)
            {
                _buttoneisible = value;
                OnPropertyChanged(nameof(Buttonenable));
            }
        }
    }


    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            if (_password != value)
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                CheckButtonEnable();
            }
        }
    }

    private void CheckButtonEnable()
    {
        Buttonenable = (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(Password));
    }

    private async void PrimaryButton_OnClick(object sender, EventArgs e)
    {
        IsButtonBusy = true;
        try
        {
            Request request = new Request()
            {
                userName = userName,
                password = Password
            };

            var response = await _loginService?.AuthenticateV1(request, false);
            if (response != null)
            {
                if ( response.Status == 1)
                {

                        try
                        {
                            await AppShell.Current.GoToAsync("///MainPage");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Navigation error: " + ex.Message);
                        }

                }
                else
                {
                    if (response.Error != null)
                    {
                        if (!string.IsNullOrEmpty(response.Error.Message))
                        {
                            MainThread.InvokeOnMainThreadAsync(async () =>
                            {
                                await ErrorPopup.ShowAsync(response.Error.Message);
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
        finally
        {
            IsButtonBusy = false;
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

    private async void LinkLabel_Clicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync("///PortalGuid");

    }

    private async void CoursesSignUpPage(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync($"///Courses?mId={1}");
    }

    private async void ForgotPasswordClicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync("///PasswordRecovery");
    }

    private async void SignupClicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync("///SignUp");
    }

    private async void SingupInMatches(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync("///leaguesList");
    }
}