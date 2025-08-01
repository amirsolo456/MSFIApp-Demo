namespace MSFIApp.Components.Controls;

public partial class TittleLabel : ContentView
{
    public TittleLabel()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {


        }
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(TittleLabel), string.Empty);

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(TittleLabel), Colors.Black);

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(TittleLabel), 14d);

    public static readonly BindableProperty FontAttributesProperty =
        BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(TittleLabel), FontAttributes.None);

    public new static readonly BindableProperty HorizontalOptionsProperty =
        BindableProperty.Create(nameof(HorizontalOptions), typeof(LayoutOptions), typeof(TittleLabel), LayoutOptions.Start);

    public new static readonly BindableProperty VerticalOptionsProperty =
        BindableProperty.Create(nameof(VerticalOptions), typeof(LayoutOptions), typeof(TittleLabel), LayoutOptions.Center);

    public string? Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    public new LayoutOptions HorizontalOptions
    {
        get => (LayoutOptions)GetValue(HorizontalOptionsProperty);
        set => SetValue(HorizontalOptionsProperty, value);
    }

    public new LayoutOptions VerticalOptions
    {
        get => (LayoutOptions)GetValue(VerticalOptionsProperty);
        set => SetValue(VerticalOptionsProperty, value);
    }
}
