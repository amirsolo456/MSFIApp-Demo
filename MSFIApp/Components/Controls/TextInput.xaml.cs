using System.ComponentModel;

namespace MSFIApp.Components.Controls;

public partial class TextInput : ContentView, INotifyPropertyChanged
{
    public event EventHandler OnFocused;
    public TextInput()
    {
        InitializeComponent();
    }
    public bool hasAnnotation = false;
    public bool firstLoad = false;

    public Color FocusedColor = Color.FromArgb("#2F54EB");
    public Color NormalColor = Color.FromArgb("#595959");

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(MSFIApp.Components.Controls.TextInput), string.Empty, BindingMode.TwoWay);

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MSFIApp.Components.Controls.TextInput), string.Empty);

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(MSFIApp.Components.Controls.TextInput), string.Empty);

    public static readonly BindableProperty InputModeProperty =
    BindableProperty.Create(nameof(InputMode), typeof(Keyboard), typeof(MSFIApp.Components.Controls.TextInput), Keyboard.Text, propertyChanged: OnInputModePropChanged);

    public static readonly BindableProperty NormalBorderColorProperty =
       BindableProperty.Create(nameof(NormalBorderColor), typeof(Color), typeof(MSFIApp.Components.Controls.TextInput), Color.FromArgb("#595959"), propertyChanged: OnNormalBorderColorPropertyChanged);

    public static readonly BindableProperty FocusedBorderColorProperty =
            BindableProperty.Create(nameof(NormalBorderColor), typeof(Color), typeof(MSFIApp.Components.Controls.TextInput), Color.FromArgb("#2F54EB"), propertyChanged: OnFocusedBorderColorPropertyChanged);

    public static readonly BindableProperty ShowErrorerProperty =
        BindableProperty.Create(nameof(ShowErrore), typeof(string), typeof(MSFIApp.Components.Controls.TextInput), "", propertyChanged: OnShowErrorerPropertyChanged);

    public static readonly BindableProperty hasBorderProperty =
    BindableProperty.Create(nameof(hasBorder), typeof(bool), typeof(MSFIApp.Components.Controls.TextInput), true);

    private static async void OnFocusedBorderColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MSFIApp.Components.Controls.TextInput input)
        {
            input.FocusedColor = (Color)newValue;
        }
    }

    private static async void OnNormalBorderColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MSFIApp.Components.Controls.TextInput input)
        {
            input.NormalColor = (Color)newValue;
        }
    }

    private static async void OnShowErrorerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MSFIApp.Components.Controls.TextInput input)
        {
            input.hasAnnotation = (newValue is null || newValue == "" ? false : true);
        }
    }

    private static async void OnInputModePropChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MSFIApp.Components.Controls.TextInput input)
        {
            if (newValue is Keyboard keyboard)
            {
                if (keyboard == Keyboard.Numeric || keyboard == Keyboard.Telephone || keyboard == Keyboard.Date)
                    input.MainEntry.FontFamily = "Yekan";
                else
                    input.MainEntry.FontFamily = "IRANSans";
            }

        }
    }

    public static readonly BindableProperty TrailingContentProperty =
    BindableProperty.Create(nameof(TrailingContent), typeof(View), typeof(TextInput), null);

    public View TrailingContent
    {
        get => (View)GetValue(TrailingContentProperty);
        set => SetValue(TrailingContentProperty, value);
    }

    public bool hasBorder
    {
        get => (bool)GetValue(hasBorderProperty);
        set => SetValue(hasBorderProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set
        { 
            SetValue(TextProperty, value);
            OnPropertyChanged(nameof(Text));
        }
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public Keyboard InputMode
    {
        get => (Keyboard)GetValue(InputModeProperty);
        set => SetValue(InputModeProperty, value);
    }

    public Color NormalBorderColor
    {
        get => (Color)GetValue(NormalBorderColorProperty);
        set => SetValue(NormalBorderColorProperty, value);
    }

    public Color FocusedBorderColor
    {
        get => (Color)GetValue(FocusedBorderColorProperty);
        set => SetValue(FocusedBorderColorProperty, value);
    }

    public string ShowErrore
    {
        get => (string)GetValue(ShowErrorerProperty);
        set => SetValue(ShowErrorerProperty, value);
    }


    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Task.Delay(50);
            MainEntry.Focus();
        });

        AnimateLabel(true);
        ManageDynamicStyles(1);
        OnFocused?.Invoke(sender, e);
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {

        if (string.IsNullOrWhiteSpace(Text))
        {
            AnimateLabel(false);
        }


        ManageDynamicStyles(2);
    }

    public void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(e.NewTextValue))
        {

            AnimateLabel(true);
            if (firstLoad && hasAnnotation)
            {
                ErroreLbl.IsVisible = false;
                TxtEntryBorder.Stroke = FocusedColor;
                FloatingLabel.TextColor = FocusedColor;
            }

            if (firstLoad == false) firstLoad = true;
        }
        else
        {
            if (firstLoad && hasAnnotation)
            {
                ErroreLbl.IsVisible = true;
                TxtEntryBorder.Stroke = Colors.Red;
                FloatingLabel.TextColor = Colors.Red;
            }
            else
                AnimateLabel(false);
        }
    }

    private async void AnimateLabel(bool show)
    {
        var targetY = show ? -10 : 20;
        var targetOpacity = show ? 1 : 0;

        Task.Run(() =>
        {
            FloatingLabel?.TranslateTo(0, targetY, 250, Easing.SinInOut);
            FloatingLabel?.FadeTo(targetOpacity, 200, Easing.SinInOut);
        });

    }

    private void Entry_Completed(object sender, EventArgs e)
    {
        ManageDynamicStyles(4);
    }


    /// <summary>
    /// Type 1 => Opacity 0.5
    /// Type 2 => Color Black
    /// Type 3 => Color Blue
    /// Type 4 => Color Blue  Color Black
    /// </summary>
    private void SetDynamicStyles(int Type)
    {
        try
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {


                if (Type == 1)
                {
                    TxtEntryBorder.Stroke = NormalColor;
                    FloatingLabel.TextColor = NormalColor;
                    FloatingLabel.Opacity = 0.8;
                    TxtEntryBorder.Opacity = 0.3;
                }
                else
                {
                    FloatingLabel.Opacity = 1;
                    TxtEntryBorder.Opacity = 1;
                    if (Type == 4 || Type == 2)
                    {
                        TxtEntryBorder.Stroke = NormalColor;
                        FloatingLabel.TextColor = NormalColor;
                    }
                    else if (Type == 3)
                    {
                        TxtEntryBorder.Stroke = FocusedColor;
                        FloatingLabel.TextColor = FocusedColor;
                    }

                }
            });
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    /// <summary>
    /// Type 1 => Focuse
    /// Type 2 => UnFocues
    /// Type 3 => TextChanged
    /// Type 4 => Completed
    /// </summary>
    private void ManageDynamicStyles(int EventType)
    {
        try
        {

            if (!string.IsNullOrEmpty(Text))
            {
                if (firstLoad && hasAnnotation)
                {
                    ErroreLbl.IsVisible = false;
                }

                if (EventType == 1 || EventType == 3)
                {
                    SetDynamicStyles(3);
                }
                else if (EventType == 4)
                {
                    SetDynamicStyles(4);
                }
                else if (EventType == 2)
                {
                    SetDynamicStyles(2);
                }
                else
                {
                    SetDynamicStyles(1);
                }
            }
            else
            {
                if (EventType == 1)
                    SetDynamicStyles(3);
                else if (EventType == 2 || EventType == 3 || EventType == 4)
                    SetDynamicStyles(1);

                if (firstLoad && hasAnnotation)
                {
                    MainEntry.PlaceholderColor = Colors.Red;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}