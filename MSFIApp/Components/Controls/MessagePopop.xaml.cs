namespace MSFIApp.Components.Controls;

public partial class MessagePopop : ContentView
{
    public MessagePopop()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty MessageTextProperty =
      BindableProperty.Create(nameof(MessageText), typeof(string), typeof(MessagePopop), default(string));

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MessagePopop), Colors.White);

    public static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MessagePopop), Colors.Black);

    public string MessageText
    {
        get => (string)GetValue(MessageTextProperty);
        set => SetValue(MessageTextProperty, value);
    }

    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public async Task ShowAsync(string message, int duration = 3000)
    {
        MessageText = message;
        IsVisible = true;
        this.TranslationY = -50;
        await this.TranslateTo(0, 0, 300, Easing.BounceOut);
        await Task.Delay(duration);
        await this.TranslateTo(0, -50, 300, Easing.CubicOut);
        IsVisible = false;
    }
}