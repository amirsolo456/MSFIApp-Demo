namespace MSFIApp.Components.Controls;

public partial class NumberInput : ContentView
{
    public NumberInput()
    {
        InitializeComponent();
    }


    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(NumberInput),
            default(string),
            BindingMode.TwoWay);

    public string? Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    private void PhoneEntry_Focused(object sender, FocusEventArgs e)
    {
        PhoneEntry.TextColor = Colors.Blue;
        Underline.BackgroundColor = Colors.Blue;
    }

    private void PhoneEntry_Unfocused(object sender, FocusEventArgs e)
    {
        PhoneEntry.TextColor = Colors.Black;
        Underline.BackgroundColor = Colors.Black;
    }
}
