using System.Windows.Input;

namespace MSFIApp.Components.Controls;

public partial class IconLabel : ContentView
{
    public IconLabel()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TextProperty =
       BindableProperty.Create(nameof(Text), typeof(string), typeof(IconLabel), string.Empty);

    public static readonly BindableProperty IconSourceProperty =
        BindableProperty.Create(nameof(IconSource), typeof(ImageSource), typeof(IconLabel), null);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(IconLabel), null);

    public static readonly BindableProperty ImageSizeProperty =
    BindableProperty.Create(nameof(ImageSize), typeof(double), typeof(IconLabel), 30.0);

    public static readonly BindableProperty FontAttributesProperty =
        BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(IconLabel), FontAttributes.None);

    public static readonly BindableProperty TextColorProperty =
    BindableProperty.Create(nameof(TextColor), typeof(Microsoft.Maui.Graphics.Color), typeof(IconLabel), Microsoft.Maui.Graphics.Color.FromArgb("0,0,0,0.5"));

    // FontSize property
    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(LinkLabel), 14d);
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ImageSource IconSource
    {
        get => (ImageSource)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public double ImageSize
    {
        get => (double)GetValue(ImageSizeProperty);
        set => SetValue(ImageSizeProperty, value);
    }

    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    public Microsoft.Maui.Graphics.Color TextColor
    {
        get => (Microsoft.Maui.Graphics.Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
}