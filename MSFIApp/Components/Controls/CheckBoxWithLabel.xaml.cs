namespace MSFIApp.Components.Controls;

public partial class CheckBoxWithLabel : ContentView
{
    public CheckBoxWithLabel()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(CheckBoxWithLabel), string.Empty);

    public static readonly BindableProperty IsCheckedProperty =
        BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBoxWithLabel), false, BindingMode.TwoWay);

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }
}
