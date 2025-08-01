using Microsoft.Maui.Controls.Shapes;

public class MessagePopupView : ContentView
{
    private Label _messageLabel;
    private Border _border;

    public MessagePopupView()
    {
        IsVisible = false;
        TranslationY = -50;

        _messageLabel = new Label
        {
            FontSize = 16,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            TextColor = Colors.White
        };

        _border = new Border
        {
            BackgroundColor = Colors.Black,
            StrokeShape = new RoundRectangle { CornerRadius = 10 },
            Padding = new Thickness(16),
            Content = _messageLabel,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
        };

        Content = new Grid
        {
            HeightRequest = 70,
            VerticalOptions = LayoutOptions.Start,
            Padding = 0,
            RowSpacing = 0,
            Children = { _border }
        };
    }

    public async Task ShowAsync(string message, int duration = 3000)
    {
        _messageLabel.Text = message;
        IsVisible = true;

        await this.TranslateTo(0, 0, 300, Easing.BounceOut);
        await Task.Delay(duration);
        await this.TranslateTo(0, -50, 300, Easing.CubicOut);

        IsVisible = false;
    }
}
