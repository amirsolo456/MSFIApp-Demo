using Microsoft.Maui.ApplicationModel;
namespace MSFIApp.Components.Controls;

public partial class NavigationLabel : ContentView
{
    public NavigationLabel()
    {
        InitializeComponent();
    }


    public static readonly BindableProperty TextProperty =
   BindableProperty.Create(nameof(Text), typeof(string), typeof(NavigationLabel), string.Empty);

    // TextColor property
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(NavigationLabel), Colors.Black);

    // FontAttributes property
    public static readonly BindableProperty FontAttributesProperty =
        BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(NavigationLabel), FontAttributes.None);

    // FontSize property
    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(NavigationLabel), 14d);

    public static readonly BindableProperty UrlAdressProperty =
BindableProperty.Create(nameof(UrlAdress), typeof(string), typeof(NavigationLabel), string.Empty);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public string UrlAdress
    {
        get => (string)GetValue(UrlAdressProperty);
        set => SetValue(UrlAdressProperty, value);
    }


    private void OnLabelTapped(object sender, EventArgs e)
    {
        OpenUrl(UrlAdress);
    }


    private async void OpenUrl(string Url)
    {
        if (!string.IsNullOrWhiteSpace(Url))
        {
            try
            {
                await Browser.OpenAsync(Url);
                //await AppShell.Current?.GoToAsync(Url);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Cannot open URL: {Url} - {ex.Message}");
            }
        }
    }
}