using MSFIApp.Dtos.BaseData.ClassRegisttration;
using MSFIApp.Services.BaseData.ClassRegistration;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.Components.Controls;

public partial class ClassRegistrationSelectBox : ContentView, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private IClassRegistrationService<Response, ObservableCollection<ResponseData>, Request> _classRegistrationService;
    private ObservableCollection<MSFIApp.Dtos.BaseData.ClassRegisttration.ResponseData> responseDatas { get; set; }
    public ClassRegistrationSelectBox()
    {
        InitializeComponent();
        this.Loaded += ClassRegSelectBox_Loaded;
    }


    private async void ClassRegSelectBox_Loaded(object? sender, EventArgs e)
    {
        this.Loaded -= ClassRegSelectBox_Loaded;
        try
        {
            _classRegistrationService = Microsoft.Maui.Controls.Application.Current?.Handler?.MauiContext?.Services?.GetService<IClassRegistrationService<Response, ObservableCollection<ResponseData>, Request>>();
            BindingContext = _classRegistrationService;
            var classResluts = await _classRegistrationService.GetClassRegistrationData(new Request());
            if (classResluts != null && classResluts.Entity != null)
                responseDatas = new ObservableCollection<ResponseData>(classResluts.Entity);
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    private void ProvincePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ProvincePicker.SelectedIndex >= 0)
        {
            var selected = ProvincePicker.SelectedItem as ResponseData;
            if (selected != null)
            {

                SelectedProvince = selected;

            }
        }
    }

    private ResponseData _selectedProvince;
    public ResponseData SelectedProvince
    {
        get => _selectedProvince;
        set
        {
            if (_selectedProvince != value)
            {
                _selectedProvince = value;
                OnPropertyChanged(); // اطمینان از notify شدن UI

            }
        }
    }
}