using MSFIApp.ViewModels.Public.Favorites;
using MSFIApp.ViewModels.User.News;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MSFIApp.Pages;

public partial class Main : ContentPage
{
    int currentIndex = 0;
    double panStartX;
    double panCurrentX;
    double panDeltaX = 0;
    private NewsViewModel _MainViewModel;
    private FavoritesViewModel _FavoritesViewModel;
    public Main(NewsViewModel mainViewModel, FavoritesViewModel favoritesViewModel)
    {
        try
        {
            InitializeComponent();
            BindingContext = mainViewModel;
            _MainViewModel = mainViewModel;
            _FavoritesViewModel = favoritesViewModel;
            UpdateTabStyles();
        }
        catch (Exception ex)
        {

        }
    }

    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            BindingContext = _MainViewModel;
            if(_MainViewModel.Response != null)
            {
                if(_MainViewModel.Response.Error != null)
                {
                    if (!string.IsNullOrEmpty(_MainViewModel.Response.Error.Message))
                    {
                        MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await ErrorPopup.ShowAsync(_MainViewModel.Response.Error.Message);
                        });
                    }
                }
                else
                {

                }
            }

            FavoritView.BindingContext = _FavoritesViewModel;

        }
        catch (Exception)
        {
            throw;
        }
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        BindingContext = _MainViewModel;
    }

    private void OnTabClicked(object sender, EventArgs e)
    {
        if (sender == BtnTab1) SlideToPage(0);
        else if (sender == BtnTab2) SlideToPage(1);
        else if (sender == BtnTab3) SlideToPage(2);
    }


    bool isHorizontalPan = false;
    void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        double width = SlideContainer.Width;
        try
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    isHorizontalPan = true;
                    panStartX = SlideContainer.TranslationX;
                    panDeltaX = 0;
                    break;

                case GestureStatus.Running:

                    if (isHorizontalPan)
                    {
                        panCurrentX = panStartX + e.TotalX;
                        panDeltaX = e.TotalX;
                    }
                    break;

                case GestureStatus.Completed:
                    if (isHorizontalPan)
                    {
                        if (panDeltaX > 20 && currentIndex < 2)
                            currentIndex++;
                        else if (panDeltaX < -20 && currentIndex > 0)
                            currentIndex--;

                        SlideToPage(currentIndex);
                        isHorizontalPan = false;
                    }
                    break;
            }
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {

        }
    }

    private async void SlideToPage(int index)
    {
        double width = SlideContainer.Width;

        if (width == 0)
        {
            await Task.Delay(50);
            width = SlideContainer.Width;
            if (width == 0) return; // باز هم مقدار نداده؟ بگذار به حال خودش.
        }


        currentIndex = index;
        double targetX = 429 * currentIndex;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await SlideContainer.TranslateTo(targetX, 0, 300, Easing.CubicInOut);
        });


        if (currentIndex == 0)
            await AnimateTabIndicator(Blackborder, BtnTab1);
        else if (currentIndex == 1)
            await AnimateTabIndicator(Blackborder, BtnTab2);
        else
            await AnimateTabIndicator(Blackborder, BtnTab3);
        UpdateTabStyles();
    }


    private async Task AnimateTabIndicator(View indicator, VisualElement tabButton)
    {
        var parent = indicator.Parent as VisualElement;
        var location = tabButton.GetBoundingBoxRelativeTo(parent);

        double targetX = -location.X;
        double targetWidth = tabButton.Width;

        var widthAnim = new Animation(v => indicator.WidthRequest = v, indicator.Width, targetWidth);
        widthAnim.Commit(indicator, "WidthAnimation", length: 300, easing: Easing.CubicInOut);

        await indicator.TranslateTo(targetX, 0, 300, Easing.CubicInOut);
    }

    private void UpdateTabStyles()
    {
        BtnTab1.FontAttributes = currentIndex == 0 ? FontAttributes.Bold : FontAttributes.None;
        BtnTab2.FontAttributes = currentIndex == 1 ? FontAttributes.Bold : FontAttributes.None;
        BtnTab3.FontAttributes = currentIndex == 2 ? FontAttributes.Bold : FontAttributes.None;
    }

    private void IconButton_Clicked(object sender, EventArgs e)
    {

    }

    private void MainHeader_MorevetClick(object sender, EventArgs e)
    {
        try
        {
            MyDrawer.IsOpen = !MyDrawer.IsOpen;
        }
        catch (Exception ex)
        {

        }
    }

    private void MainHeader_FoureVetClick(object sender, EventArgs e)
    {

    }

    private void MainHeader_LogoClick(object sender, EventArgs e)
    {

    }
}

public static class VisualElementExtensions
{
    public static Rect GetBoundingBoxRelativeTo(this VisualElement view, VisualElement parent)
    {
        var transform = view.GetBoundsInWindow();
        var parentTransform = parent.GetBoundsInWindow();
        return new Rect(transform.X - parentTransform.X, transform.Y - parentTransform.Y, transform.Width, transform.Height);
    }

    public static Rect GetBoundsInWindow(this VisualElement view)
    {
        var location = view.GetLocationOnScreen();
        return new Rect(location.X, location.Y, view.Width, view.Height);
    }

    public static Point GetLocationOnScreen(this VisualElement view)
    {
        var x = 0.0;
        var y = 0.0;
        var current = view;
        while (current != null)
        {
            x += current.X;
            y += current.Y;
            current = current.Parent as VisualElement;
        }
        return new Point(x, y);
    }


}


