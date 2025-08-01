namespace MSFIApp.Components.Controls;

public partial class Drawer : ContentView
{
    public enum DrawerDirection
    {
        Left,
        Right
    }
    public Drawer()
    {
        InitializeComponent();
    }
    public static readonly BindableProperty DrawerContentViewProperty = BindableProperty.Create(
    nameof(DrawerContentView),
    typeof(View),
    typeof(Drawer),
    propertyChanged: OnDrawerContentViewChanged);

    public View DrawerContentView
    {
        get => (View)GetValue(DrawerContentViewProperty);
        set => SetValue(DrawerContentViewProperty, value);
    }

    public static readonly BindableProperty DirectionProperty = BindableProperty.Create(
    nameof(Direction),
    typeof(DrawerDirection),
    typeof(Drawer),
    DrawerDirection.Right,
    propertyChanged: OnDirectionChanged);

    public DrawerDirection Direction
    {
        get => (DrawerDirection)GetValue(DirectionProperty);
        set => SetValue(DirectionProperty, value);
    }


    public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
        nameof(IsOpen),
        typeof(bool),
        typeof(Drawer),
        false,
        propertyChanged: OnIsOpenChanged);

    public bool IsOpen
    {
        get => (bool)GetValue(IsOpenProperty);
        set => SetValue(IsOpenProperty, value);
    }

    private static void OnIsOpenChanged(BindableObject bindable, object oldValue, object newValue)
    {
        try
        {
            var drawer = (Drawer)bindable;
            bool isOpen = (bool)newValue;

            if (isOpen)
                drawer.Show();
            else
                drawer.Hide();
        }
        catch (Exception ex)
        {

        }
    }

    private async void Show()
    {
        try
        {
            IsVisible = true;

            // بسته به جهت، از کدوم طرف وارد بشه
            double fromX = Direction == DrawerDirection.Right ? DrawerPanel.Width : -DrawerPanel.Width;
            DrawerPanel.TranslationX = fromX;

            await DrawerPanel.TranslateTo(0, 0, 250, Easing.CubicOut);
        }
        catch (Exception ex)
        {

        }
    }

    private async void Hide()
    {
        try
        {
            double toX = Direction == DrawerDirection.Right ? DrawerPanel.Width : -DrawerPanel.Width;

            await DrawerPanel.TranslateTo(toX, 0, 250, Easing.CubicIn);
            IsVisible = false;
        }
        catch (Exception ex)
        {

        }
    }

    private static void OnDirectionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var drawer = (Drawer)bindable;

        // مثلاً می‌تونی Translation اولیه رو تنظیم کنی
        if (!(bool)drawer.IsOpen)
        {
            drawer.DrawerPanel.TranslationX = (DrawerDirection)newValue == DrawerDirection.Right
                ? drawer.DrawerPanel.Width
                : -drawer.DrawerPanel.Width;
        }
    }

    private static void OnDrawerContentViewChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Drawer drawer && newValue is View content)
        {
            drawer.DrawerContent.Content = content;
        }
    }

    private void OnCloseClicked(object sender, EventArgs e)
    {
        try
        {
            IsOpen = false;
        }
        catch (Exception ex)
        {

        }
    }
}
