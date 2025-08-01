using MSFIApp.ViewModels.User.SignUp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static MSFIApp.Components.Controls.CustomRadioButton;

namespace MSFIApp.Pages;

public partial class SignUp : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    private SignUpViewModel _SignUpViewModel;

    private MSFIApp.Dtos.User.SignUp.Request _request = new Dtos.User.SignUp.Request();
    public MSFIApp.Dtos.User.SignUp.Request Request
    {
        get => _request;
        set
        {
            _request = value;
            OnPropertyChanged(nameof(Request));
        }
    }

    private ObservableCollection<CustomRadioItems> _customRadioItems;
    public ObservableCollection<CustomRadioItems> CustomRadioItems
    {
        get => _customRadioItems;
        set
        {
            _customRadioItems = value;
            OnPropertyChanged(nameof(CustomRadioItems));
        }
    }

    public SignUp(SignUpViewModel signUpViewModel)
    {
        try
        {
            InitializeComponent();
            CustomRadioItems = new ObservableCollection<CustomRadioItems>()
            {
                new CustomRadioItems(1,"خانم"),
                new CustomRadioItems(2,"آقا")
            };

            _SignUpViewModel = signUpViewModel;
        }
        catch (Exception ex)
        {

        }
    }

    private MSFIApp.Dtos.BaseData.Areas.ResponseData _areaselectedProvince;
    public MSFIApp.Dtos.BaseData.Areas.ResponseData AreaselectedProvince
    {
        get => _areaselectedProvince;
        set
        {

            _areaselectedProvince = value;
            areaID = AreaselectedProvince?.Id ?? 0;
            OnPropertyChanged(nameof(AreaselectedProvince)); // اطمینان از notify شدن UI

        }
    }

    private MSFIApp.Dtos.BaseData.Areas.ResponseData _citiesselectedProvince;
    public MSFIApp.Dtos.BaseData.Areas.ResponseData CitieslectedProvince
    {
        get => _citiesselectedProvince;
        set
        {
            _citiesselectedProvince = value;
            OnPropertyChanged(nameof(CitieslectedProvince)); // اطمینان از notify شدن UI
        }
    }


    public static readonly BindableProperty areaIDProperty =
        BindableProperty.Create(nameof(areaID), typeof(int), typeof(SignUp), 0, propertyChanged: OnareaIDChanged);

    public int areaID
    {
        get => (int)GetValue(areaIDProperty);
        set
        {
            SetValue(areaIDProperty, value);
            OnPropertyChanged(nameof(areaID));
        }
    }

    public static readonly BindableProperty IsButtonBusyProperty =
        BindableProperty.Create(nameof(IsButtonBusy), typeof(bool), typeof(SignUp),false);
    public bool IsButtonBusy
    {
        get => (bool)GetValue(IsButtonBusyProperty);
        set
        {
            SetValue(IsButtonBusyProperty, value);
            OnPropertyChanged(nameof(IsButtonBusy));
        }
    }

    private static void OnareaIDChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (SignUp)bindable;
        if (newValue != null && newValue is string val && !string.IsNullOrEmpty(val))
        {

        }
    }

    private async void GetBackClick(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync("///Login");
    }

    private async void LinkLabel_Clicked(object sender, EventArgs e)
    {
        await Task.Yield();
        var datePickerPage = new MauiDatePicker.Pages.DatePickerFa();
        var pushTask = Navigation.PushModalAsync(datePickerPage);
        var selectedDate = await datePickerPage.ShowAsync();

        if (selectedDate != null)
        {
            //await DisplayAlert("تاریخ انتخاب شده", selectedDate.PersianDate.ToString(), "باشه");
        }
        else
        {
           // await DisplayAlert("پیغام", "تاریخی انتخاب نشد", "باشه");
        }
    }


    private string CheckData()
    {
        string Errors = "";
        try
        {
            if (CitieslectedProvince == null)
            {
                Errors +=  "استان محل سکونت وارد نشده است" + "\n" ;
            }
            else
            {
                Request.AreaId = CitieslectedProvince.Id ?? 0;
            }

            if (AreaselectedProvince == null)
            {
                Errors +=  "شهر محل سکونت وارد نشده است" + "\n";
            }
            else
            {
                Request.AreaId = areaID;
            }

            if (Request is null)
            {
                Errors +=  "اطلاعات وارد نشده است" + "\n";
            }
            else
            {
                if (string.IsNullOrEmpty(Request.Mobile))
                {
                    Errors +=  "شماره تلفن همراه وارد نشده است" + "\n";
                }

                if (string.IsNullOrEmpty(Request.FirstName))
                {
                    Errors +=  "نام وارد نشده است" + "\n";
                }

                if (string.IsNullOrEmpty(Request.LastName))
                {
                    Errors +=  "نام فامیلی وارد نشده است" + "\n";
                }

                if (string.IsNullOrEmpty(Request.BirthDay))
                {
                    Errors += "تاریخ تولد وارد نشده است" + "\n";
                }
            }
        }
        catch (Exception ex)
        {
 
        }

        return Errors;
    }

    private async void PrimaryButton_OnClick(object sender, EventArgs e)
    {
        IsButtonBusy = true;
        try
        {
            string errors = CheckData();
            if (string.IsNullOrEmpty(errors))
            {
                var response = await _SignUpViewModel?.GoRegister(Request);
                if (response != null)
                {
                    if (response.IsFailure)
                    {
                        MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await ErrorPopup?.ShowAsync(response.Error.Message);
                        });
                    }
                    else // signup
                    {
                        int? userId = response.Id;
                        if (userId != null)
                        {
                            await AppShell.Current.GoToAsync($"///ConfirmationCode?UserPhone={Request.Mobile}&UserId={userId}");
                        }
                        //MainThread.InvokeOnMainThreadAsync(async () =>
                        //{
                        //    await ErrorPopup?.ShowAsync("ثبت نام با موفقیت انجام شد");
                        //});
                    }
                }
            }
            else
            {
                MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await ErrorPopup?.ShowAsync(errors);
                });
            }
            //_SignUpViewModel?.ShowMessageAsync(new Exception(message: "خطا در برقراری ارتباط با سرور"));
        }
        catch (Exception)
        {

        }
        finally
        {
            IsButtonBusy = false;
        }
    }
}