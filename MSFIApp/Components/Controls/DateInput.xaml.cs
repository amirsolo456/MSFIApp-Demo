using System.Diagnostics;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace MSFIApp.Components.Controls;

public partial class DateInput : ContentView
{
    private MauiDatePicker.Pages.DatePickerFa datePicker;
    public DateInput()
    {
        InitializeComponent();

        this.Loaded += DateInput_Loaded;

    }

    private async void DateInput_Loaded(object? sender, EventArgs e)
    {
        datePicker = new MauiDatePicker.Pages.DatePickerFa();
    }

    public static readonly BindableProperty SelectedDateTextProperty =
       BindableProperty.Create(nameof(SelectedDateText), typeof(string), typeof(DateInput), default(string));

    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(DateInput), default(string));

    public string SelectedDateText
    {
        get => (string)GetValue(SelectedDateTextProperty);
        set => SetValue(SelectedDateTextProperty, value);
    }

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }


    private bool _isDatePickerOpen = false;

    private async void OnDateEntryClicked(object sender, EventArgs e)
    {
        if (_isDatePickerOpen)
            return;

        LoadingIndicator.IsVisible = true;
        _isDatePickerOpen = true;
        await Task.Delay(1000);
        try
        {

            var navigation = Navigation.PushModalAsync(datePicker);
            var selectedDate = await datePicker.ShowAsync();
            if (selectedDate != null)
            {
                SelectedDateText = selectedDate.ToString();
            }
        }
        catch (Exception ex)
        {
            // هندل مناسب
            Debug.WriteLine(ex);
        }
        finally
        {
            LoadingIndicator.IsVisible = false;

            _isDatePickerOpen = false;
        }
    }


    private void TextInput_OnFocused(object sender, EventArgs e)
    {
        OnDateEntryClicked(sender,e);
    }
}