namespace MSFIApp.Components.Controls;

public partial class PasswordInput : ContentView
{
    readonly Color FocusedColor = Color.FromArgb("#2F54EB");
    readonly Color NormalColor = Colors.Black;
    public PasswordInput()
    {
        InitializeComponent();
        UpdateIcon();
        UpdateFloatingLabel();
        //this.BindingContext = this;
    }


    public static readonly BindableProperty PasswordProperty =
        BindableProperty.Create(nameof(Password), typeof(string), typeof(PasswordInput), string.Empty, BindingMode.TwoWay);

    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(PasswordInput), true, propertyChanged: OnIsPasswordChanged);

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(PasswordInput), string.Empty);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(PasswordInput), string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string EyeIcon => IsPassword ? "eyehidden.png" : "eyeshow.svg";

    private static void OnIsPasswordChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //ToggleButton.Source = 
        if (bindable is PasswordInput passwordInput)
        {
            passwordInput.UpdateIcon();
        }
    }

    private void OnTogglePasswordVisibilityClicked(object sender, EventArgs e)
    {
        IsPassword = !IsPassword;
    }

    private void UpdateIcon()
    {
        ToggleButton.Source = EyeIcon;
        PasswordEntry.IsPassword = IsPassword;
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
            if (Type == 1)
            {
                PassEntryBorder.Stroke = NormalColor;
                FloatingLabel.TextColor = NormalColor;
                FloatingLabel.Opacity = 0.8;
                PassEntryBorder.Opacity = 0.3;
            }
            else
            {
                PassEntryBorder.Stroke = NormalColor;
                FloatingLabel.TextColor = NormalColor;
                FloatingLabel.Opacity = 1;
                PassEntryBorder.Opacity = 1;


                if (Type == 4 || Type == 2)
                {
                    PassEntryBorder.Stroke = FocusedColor;
                    FloatingLabel.TextColor = NormalColor;
                }
                else if (Type == 3)
                {
                    PassEntryBorder.Stroke = FocusedColor;
                    FloatingLabel.TextColor = FocusedColor;
                }
            }

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

            if (!string.IsNullOrEmpty(Password))
            {
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
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Task.Delay(50);
            PasswordEntry.Focus();
        });
        AnimateLabel(true);
        ManageDynamicStyles(1);
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Password))
            AnimateLabel(false);

        ManageDynamicStyles(2);
    }
    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateFloatingLabel();
    }

    private void UpdateFloatingLabel()
    {
        if (!string.IsNullOrWhiteSpace(Password))
        {
            AnimateLabel(true);
        }
        else
        {
            AnimateLabel(false);
        }
    }


    private async void AnimateLabel(bool show)
    {
        var targetY = show ? -10 : 20;
        var targetOpacity = show ? 1 : 0;
        Task.Run(() =>
        {
            FloatingLabel.TranslateTo(0, targetY, 300, Easing.SinInOut);
            FloatingLabel.FadeTo(targetOpacity, 200, Easing.SinInOut);
        });

    }

    private void Entry_Completed(object sender, EventArgs e)
    {
        ManageDynamicStyles(4);
    }
}
