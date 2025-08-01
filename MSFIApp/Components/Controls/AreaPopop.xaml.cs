using CommunityToolkit.Maui.Views;
using MSFIApp.Dtos.BaseData.Areas;
using MSFIApp.Services.Common;
using System.Collections.ObjectModel;
#if ANDROID
using Android.Views;
#endif
namespace MSFIApp.Components.Controls;

public partial class AreaPopup : ContentView, IOnPageKeyDown
{
 
    public event EventHandler<ResponseData> OnSelected;
    public AreaPopup()
    {
        InitializeComponent();
        Hide();
    }

#if ANDROID
    public bool OnPageKeyDown(Keycode keyCode, KeyEvent e)
    { 
        Hide();
        return true;
    }

#endif

    public static readonly BindableProperty AreasProperty =
        BindableProperty.Create(
            nameof(Areas),
            typeof(ObservableCollection<ResponseData>),
            typeof(AreaPopup),
            new ObservableCollection<ResponseData>(), propertyChanged: OnAreasPropertyChanged);

    public ObservableCollection<ResponseData> Areas
    {
        get => (ObservableCollection<ResponseData>)GetValue(AreasProperty);
        set => SetValue(AreasProperty, value);
    }

    private static void OnAreasPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AreaPopup)bindable;
        if (newValue is ObservableCollection<ResponseData> val)
        {
            control.BuildContents(val);
        }
    }

    public void BuildContents(ObservableCollection<ResponseData> value)
    {
        ContentLayout.Clear();

        foreach (ResponseData item in value)
        {
            var lbl = new Label()
            {
                Text = item.Title,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Margin=10
            };

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                SelectedProvince = item;
                Hide();
            };

            lbl.GestureRecognizers.Add(tapGesture);
            ContentLayout.Children.Add(lbl);
        }
    }


    private ResponseData _selectedProvince;
    public ResponseData SelectedProvince
    {
        get => _selectedProvince;
        set
        {
            _selectedProvince = value;
            OnSelected?.Invoke(this,value);
        }
    }


    public async void Show()
    {
        if (IsVisible)
            return;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            this.Opacity = 0;
            this.TranslationY = -20;
            IsVisible = true;

            await Task.WhenAll(
                this.FadeTo(1, 250, Easing.CubicOut),
                this.TranslateTo(0, 0, 250, Easing.CubicOut)
            );
        });
    }

    public async void Hide()
    {
        if (!IsVisible)
            return;

        await Task.WhenAll(
            this.FadeTo(0, 200, Easing.CubicIn),
            this.TranslateTo(0, -20, 200, Easing.CubicIn)
        );

        IsVisible = false;
    }

    public void Toggle()
    {
        if (IsVisible)
            Hide();
        else
            Show();
    }
}


