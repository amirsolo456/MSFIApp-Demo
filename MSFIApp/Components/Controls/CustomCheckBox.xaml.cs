using System.ComponentModel;

namespace MSFIApp.Components.Controls;

public partial class CustomCheckBox : ContentView, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CustomCheckBox), false, BindingMode.TwoWay, propertyChanged: OnIsCheckedChanged);

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }
    private static async void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CustomCheckBox customCheckBox)
        {
            bool isChecked = (bool)newValue;
            await customCheckBox.AnimateCheckChange(isChecked);
        }
    }
    public CustomCheckBox()
    {
        InitializeComponent();
    }

    private async void OnTapped(object sender, EventArgs e)
    {
        IsChecked = !IsChecked;
        await AnimateCheckChange(IsChecked);
        OnPropertyChanged(nameof(IsChecked));
    }

    private async Task AnimateCheckChange(bool isChecked)
    {
        if (isChecked)
        {
            CheckedOverlay.IsVisible = true;
            CheckedOverlay.Scale = 0;
            await CheckedOverlay.ScaleTo(1, 150, Easing.SpringOut);
        }
        else
        {
            await CheckedOverlay.ScaleTo(0, 150, Easing.CubicIn);
            CheckedOverlay.IsVisible = false;
        }
    }


    public static readonly BindableProperty LabelTextProperty =
       BindableProperty.Create(nameof(LabelText), typeof(string), typeof(CustomCheckBox), string.Empty, propertyChanged: OnLabelTxtChanged);


    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    private static async void OnLabelTxtChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CustomCheckBox customCheckBox)
        {
            customCheckBox.CheckLbl.Text = newValue.ToString();
        }
    }
}
