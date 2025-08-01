using System.Threading.Tasks;

namespace MSFIApp.Components.Controls;

public partial class LinkLabel : ContentView
{
    public LinkLabel()
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {

        }
    }

    // Text property
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(LinkLabel), string.Empty);

    // TextColor property
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(LinkLabel), Colors.Black);

    // FontAttributes property
    public static readonly BindableProperty FontAttributesProperty =
        BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(LinkLabel), FontAttributes.None);

    // FontSize property
    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(LinkLabel), 14d);

    // TextAlignment property
    public static readonly BindableProperty TextAlignmentProperty =
        BindableProperty.Create(nameof(TextAlignment), typeof(TextAlignment), typeof(LinkLabel), TextAlignment.Center);

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

    public TextAlignment TextAlignment
    {
        get => (TextAlignment)GetValue(TextAlignmentProperty);
        set => SetValue(TextAlignmentProperty, value);
    }

    public event EventHandler Clicked;

    private async void OnLabelTapped(object sender, EventArgs e)
    {
        try
        {
            await Task.Run(() =>
            {
                Clicked?.Invoke(this, EventArgs.Empty);
            });

        }
        catch  (Exception ex)
        {
 
        }

    }
}
