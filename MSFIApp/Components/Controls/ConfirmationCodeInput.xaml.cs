using Microsoft.VisualBasic;

namespace MSFIApp.Components.Controls;

public partial class ConfirmationCodeInput : ContentView
{
    private const int CodeLength = 5;
    private readonly Entry[] _entries = new Entry[CodeLength];
    private readonly BoxView[] _boxes = new BoxView[CodeLength];
    public ConfirmationCodeInput()
    {
        try
        {
            InitializeComponent();
            BuildInputs();
        }
        catch (Exception ex)
        {


        }
    }

    public static readonly BindableProperty CodeProperty = BindableProperty.Create(
      nameof(Code),
      typeof(string),
      typeof(ConfirmationCodeInput),
      default(string),
      BindingMode.TwoWay);

    public string? Code
    {
        get => (string)GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }

    private void BuildInputs()
    {
        try
        {

            EntryStack.Children.Clear();
            var FirstEntry = new Entry();
            var LastEntry = new Entry();
            for (int i = 0; i < CodeLength; i++)
            {
                var entry = new Entry
                {
                    WidthRequest = 40,
                    MaxLength = 1,
                    FontSize = 28,
                    Keyboard = Keyboard.Numeric,
                    FontFamily = "Yekan",
                    HorizontalTextAlignment = TextAlignment.Center,
                    BackgroundColor = Colors.Transparent
                };

                var box = new BoxView
                {
                    HeightRequest = 2,
                    Color = Colors.Black
                };

                var stack = new VerticalStackLayout
                {
                    Spacing = 2,
                    Children = { entry, box }
                };

                int index = i;
                entry.Focused += (_, _) => box.Color = Colors.Blue;
                entry.Unfocused += (_, _) => box.Color = Colors.Black;

                entry.TextChanged += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.NewTextValue))
                    {
                        if (e.NewTextValue.Length > 1)
                            entry.Text = e.NewTextValue[^1..];

                        if (index < CodeLength - 1)
                            _entries[index + 1].Focus();
                    }
                };

                entry.Completed += (_, _) =>
                {
                    if (index < CodeLength - 1)
                        _entries[index + 1].Focus();
                };

                entry.Unfocused += (_, _) =>
                {
                    if (string.IsNullOrEmpty(entry.Text))
                        box.Color = Colors.Black;
                };

                entry.TextChanged += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.NewTextValue))
                    {
                        if (e.NewTextValue.Length > 1)
                            entry.Text = e.NewTextValue[^1..];

                        if (index < CodeLength - 1)
                            _entries[index + 1].Focus();
                    }

                    Code = string.Concat(_entries.Select(x => x.Text));
                };

                _entries[i] = entry;
                _boxes[i] = box;
                EntryStack.Children.Add(stack);

                if (i == 0)
                {
                    try
                    {
                        FirstEntry = entry;
                    }
                    catch
                    {
                    }
                }
                else if(i == CodeLength - 1)
                {
                    try
                    {
                        LastEntry = entry;
                    }
                    catch  
                    {
                    }
                }
            }

            Dispatcher?.Dispatch(() =>
            {
                FirstEntry?.Focus();
                LastEntry.ReturnType = ReturnType.Done;
            });
        }
        catch (Exception ex)
        {


        }
    }

    public void ShowError(string message)
    {
        try
        {
            for (int i = 0; i < CodeLength; i++)
            {
                _entries[i].TextColor = Colors.Red;
                _boxes[i].Color = Colors.Red;
            }

            ErrorLabel.Margin = new Thickness(0, 25, 0, 0);
            ErrorLabel.Text = message;
            ErrorLabel.IsVisible = true;
        }
        catch (Exception ex)
        {


        }
    }

    public void ClearError()
    {
        try
        {
            for (int i = 0; i < CodeLength; i++)
            {
                _entries[i].TextColor = Colors.Black;
                _boxes[i].Color = Colors.Black;
            }

            ErrorLabel.IsVisible = false;
            ErrorLabel.Text = string.Empty;
        }
        catch (Exception ex)
        {


        }

    }

    public void HandleBackspace()
    {
        try
        {
            for (int i = 0; i < _entries.Length; i++)
            {
                if (_entries[i].IsFocused && string.IsNullOrEmpty(_entries[i].Text))
                {
                    if (i > 0)
                    {
                        _entries[i - 1].Text = string.Empty;
                        _entries[i - 1].Focus();
                    }
                    break;
                }
            }
        }
        catch (Exception ex)
        {


        }

    }
}