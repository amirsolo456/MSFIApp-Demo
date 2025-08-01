namespace MSFIApp.Components.Controls;

public partial class Loading : ContentView
{
    public event EventHandler TryAgainFunc;
    public Loading()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
        nameof(IconSource), typeof(ImageSource), typeof(Loading), default(ImageSource));

    public ImageSource IconSource
    {
        get => (ImageSource)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(
        nameof(IconSize), typeof(double), typeof(Loading), 30.0);

    public double IconSize
    {
        get => (double)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }


    public static readonly BindableProperty TurnonRetryProperty = BindableProperty.Create(
    nameof(TurnonRetry), typeof(bool), typeof(Loading),false);

    public bool TurnonRetry
    {
        get => (bool)GetValue(TurnonRetryProperty);
        set => SetValue(TurnonRetryProperty, value);
    }


    public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(
        nameof(IsRunning), typeof(bool), typeof(Loading), false, propertyChanged: OnIsRunningChanged);

    public bool IsRunning
    {
        get => (bool)GetValue(IsRunningProperty);
        set => SetValue(IsRunningProperty, value);
    }

    private static void OnIsRunningChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Loading loading)
        {
            if ((bool)newValue)
                loading.StartRotation();
            else
                loading.StopRotation();
        }
    }

    private bool _isRotating = false;

    private async void StartRotation()
    {
        _isRotating = true;

        while (_isRotating)
        {
            await LoadingIcon.RotateTo(360, 800, Easing.Linear);
            LoadingIcon.Rotation = 0;
        }
    }

    private void StopRotation()
    {
        _isRotating = false;
        LoadingIcon.Rotation = 0;
    }

    public static readonly BindableProperty IsVisibleExternallyProperty = BindableProperty.Create(
    nameof(IsVisibleExternally), typeof(bool), typeof(Loading), false, propertyChanged: OnVisibilityChanged);

    public bool IsVisibleExternally
    {
        get => (bool)GetValue(IsVisibleExternallyProperty);
        set => SetValue(IsVisibleExternallyProperty, value);
    }

    private static void OnVisibilityChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is Loading loading)
        {
            var isVisible = (bool)newValue;

            loading.IsVisible = isVisible; // این مهمه 👈

            if (isVisible)
            {
                loading.IsRunning = true;
            }
            else
            {
                loading.IsRunning = false;
            }
        }
    }

    private void LinkLabel_Clicked(object sender, EventArgs e)
    {
        if (TryAgainFunc != null)
        {
            TryAgainFunc.Invoke(sender, e);
        }

        //MainThread.InvokeOnMainThreadAsync(() =>
        //{
        //    TurnonRetry = false;
        //});  
    }
}
