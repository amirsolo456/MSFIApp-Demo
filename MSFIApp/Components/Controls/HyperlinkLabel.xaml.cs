namespace MSFIApp.Components.Controls;

public partial class HyperlinkLabel : ContentView
{
    public HyperlinkLabel()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty TextProperty =
    BindableProperty.Create(nameof(Text), typeof(string), typeof(HyperlinkLabel), string.Empty);

    // TextColor property
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(HyperlinkLabel), Colors.Black);

    // FontAttributes property
    public static readonly BindableProperty FontAttributesProperty =
        BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(HyperlinkLabel), FontAttributes.None);

    // FontSize property
    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(HyperlinkLabel), 14d);

    public static readonly BindableProperty UrlAdressProperty =
BindableProperty.Create(nameof(UrlAdress), typeof(string), typeof(HyperlinkLabel), string.Empty);

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
                using var httpClient = new HttpClient();
                var fileBytes = await httpClient.GetByteArrayAsync(Url);

                var fileName = "downloaded_file.pdf";
                var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

                await File.WriteAllBytesAsync(filePath, fileBytes);

                await Launcher.Default?.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
            catch (Exception ex)
            {
                // می‌تونی لاگ بزنی یا پیامی نشون بدی
                System.Diagnostics.Debug.WriteLine($"Cannot open URL: {Url} - {ex.Message}");
            }
        }
    }
}