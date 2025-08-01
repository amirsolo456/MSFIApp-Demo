namespace MSFIApp.Components.Controls;

public partial class CounterLabel : ContentView
{
    public CounterLabel()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {


        }   
    }

    private const int DefaultDuration = 10;

    public static readonly BindableProperty StartCountdownProperty =
    BindableProperty.Create(
        nameof(StartCountdown),
        typeof(bool),
        typeof(CounterLabel),
        false,
        BindingMode.TwoWay, // ← خیلی مهم!
        propertyChanged: OnStartCountdownChanged);

    public bool StartCountdown
    {
        get => (bool)GetValue(StartCountdownProperty);
        set => SetValue(StartCountdownProperty, value);
    }

    private static async void OnStartCountdownChanged(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            var control = (CounterLabel)bindable;
            bool shouldStart = (bool)newValue;

            if (shouldStart)
            {
                control.CountdownText.IsVisible = true;

                for (int i = DefaultDuration; i >= 0; i--)
                {
                    control.CountdownText.Text = $"تا درخواست مجدد {i} ثانیه باقی‌مانده";
                    await Task.Delay(1000);
                }

                control.CountdownText.IsVisible = false;

                // چون BindingMode = TwoWay هست، این مقدار به ViewModel هم برمی‌گرده
                control.StartCountdown = false;
            }
        }
        catch (Exception ex)
        {


        }
    }
}