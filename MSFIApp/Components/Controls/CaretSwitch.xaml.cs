using System.Windows.Input;

namespace MSFIApp.Components.Controls;

public partial class CaretSwitch : ContentView
{
    public CaretSwitch()
    {
        InitializeComponent();
        ToggleCommand = new Command(() => IsExpanded = !IsExpanded);
        this.BindingContext = this;
    }

    public static readonly BindableProperty IsExpandedProperty =
        BindableProperty.Create(nameof(IsExpanded), typeof(bool), typeof(CaretSwitch), false, propertyChanged: OnIsExpandedChanged);

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public ICommand ToggleCommand { get; }

    private static async void OnIsExpandedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is CaretSwitch control && newValue is bool isExpanded)
        {
            uint duration = 250;
            double rotation = isExpanded ? 180 : 0;
            await control.ArrowImage.RotateTo(rotation, duration, Easing.CubicInOut);
        }
    }
}


//<controls:CaretSwitch IsExpanded = "{Binding MyBoolProperty, Mode=TwoWay}" />