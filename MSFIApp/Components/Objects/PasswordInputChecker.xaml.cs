using System.Text.RegularExpressions;

namespace MSFIApp.Components.Objects;

public partial class PasswordInputChecker : ContentView
{
    private ImageSource NotSourceGray = "notokgray.png";
    private ImageSource NotSourceRed = "notokred.png";
    private ImageSource OkSourceGray = "okgray.png";
    private ImageSource OkSourceGreen = "okgreen.png";

    public PasswordInputChecker()
    {
        InitializeComponent();
        BindingContext = this;
    }


    public static readonly BindableProperty PasswordProperty = BindableProperty.Create(
nameof(Password), typeof(string), typeof(PasswordInputChecker), string.Empty, propertyChanged: OnPasswordPropertyChanged);

    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }
    private static void OnPasswordPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PasswordInputChecker)bindable;
        control.UpdateValidation();
    }



    public static readonly BindableProperty RePasswordProperty = BindableProperty.Create(
nameof(RePassword), typeof(string), typeof(PasswordInputChecker), string.Empty, propertyChanged: OnRePasswordPropertyChanged);

    public string RePassword
    {
        get => (string)GetValue(RePasswordProperty);
        set => SetValue(RePasswordProperty, value);
    }
    private static void OnRePasswordPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PasswordInputChecker)bindable;
        control.UpdateValidation();
    }


    public static readonly BindableProperty Cond1ColorColorProperty = BindableProperty.Create(
    nameof(Cond1Color), typeof(Color), typeof(PasswordInputChecker), Colors.Gray, propertyChanged: OnCond1ColorColorPropertyChanged);

    public Color Cond1Color
    {
        get => (Color)GetValue(Cond1ColorColorProperty);
        set => SetValue(Cond1ColorColorProperty, value);
    }

    private static void OnCond1ColorColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PasswordInputChecker)bindable;
        if (newValue is Color color)
        {
            control.Cond1Img.Source = control.GetSource((color == Colors.Gray ? 0 : (color == Colors.Red ? 1 : 3)));
        }
    }


    public static readonly BindableProperty Cond2ColorColorProperty = BindableProperty.Create(
nameof(Cond2Color), typeof(Color), typeof(PasswordInputChecker), Colors.Gray, propertyChanged: OnCond2ColorColorPropertyChanged);

    public Color Cond2Color
    {
        get => (Color)GetValue(Cond2ColorColorProperty);
        set => SetValue(Cond2ColorColorProperty, value);
    }

    private static void OnCond2ColorColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PasswordInputChecker)bindable;
        if (newValue is Color color)
        {
            control.Cond2Img.Source = control.GetSource((color == Colors.Gray ? 0 : (color == Colors.Red ? 1 : 3)));
        }
    }


    public static readonly BindableProperty Cond3ColorColorProperty = BindableProperty.Create(
nameof(Cond3Color), typeof(Color), typeof(PasswordInputChecker), Colors.Gray, propertyChanged: OnCond3ColorColorPropertyChanged);

    public Color Cond3Color
    {
        get => (Color)GetValue(Cond3ColorColorProperty);
        set => SetValue(Cond3ColorColorProperty, value);
    }

    private static void OnCond3ColorColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PasswordInputChecker)bindable;
        if (newValue is Color color)
        {
            control.Cond3Img.Source = control.GetSource((color == Colors.Gray ? 2 :( color == Colors.Red ? 1: 3)));
        }
    }

    void UpdateValidation()
    {
        string pwd = Password ?? "";
        string rep = RePassword ?? "";
        bool okLength = false, okComplex = false, okMatch = false;

        if (string.IsNullOrEmpty(pwd))
        {
            Cond1Color = Colors.Gray;
            Cond2Color = Colors.Gray;
        }
        else
        {
             okLength = pwd.Length >= 8;
            if (!okLength)
                Cond1Color = Colors.Red;
            else
                Cond1Color = Colors.Green;
            // شرط 2: شامل عدد، حرف انگلیسی و کاراکتر ویژه
            okComplex = Regex.IsMatch(pwd, @"[0-9]") &&
                             Regex.IsMatch(pwd, @"[A-Za-z]") &&
                             Regex.IsMatch(pwd, @"[!@#$%^&*]");
            if (!okComplex)
                Cond2Color = Colors.Red;
            else
                Cond2Color = Colors.Green;
        }


        if (string.IsNullOrEmpty(rep))
            Cond3Color = Colors.Gray;
        else
        {
             okMatch = (pwd == rep);
            if (!okMatch)
                Cond3Color = Colors.Red;
            else Cond3Color = Colors.Green;
        }

        SubmitButton.IsEnabled = okLength && okComplex && okMatch;
        SubmitButton.BackgroundColor = SubmitButton.IsEnabled
            ? Color.FromArgb("#0078D4") // رنگ سبز-آبی یا primary شما
            : Colors.LightGray;
    }


    private ImageSource GetSource(int Type = 0)
    {
        switch (Type)
        {
            case 0:
                return NotSourceGray;
                break;
            case 1:
                return NotSourceRed;
                break;
            case 2:
                return OkSourceGray;
                break;
            case 3:
                return OkSourceGreen;
                break;
            default:
                return NotSourceGray;
                break;
        }
    }

    void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateValidation();
    }
    public event EventHandler<string> Submit;
    void OnSubmitClicked(object sender, EventArgs e)
    {
        Submit?.Invoke(this, Password);
    }
}