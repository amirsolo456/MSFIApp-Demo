using MSFIApp.Dtos.Public.EducationList;
using MSFIApp.Services.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MSFIApp.Components.Objects;

public partial class CourseCardView : ContentView, INotifyPropertyChanged, ILoadingTryAgainService.All_In
{
    public new event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public static readonly BindableProperty SharedListProperty =
       BindableProperty.Create(
           nameof(SharedList),
           typeof(ObservableCollection<ResponseData>),
           typeof(CourseCardView),
           new ObservableCollection<ResponseData>(),
           propertyChanged: OnSharedListChanged        
       );

    public static readonly BindableProperty MenListProperty =
        BindableProperty.Create(
            nameof(MenList),
            typeof(ObservableCollection<ResponseData>),
            typeof(CourseCardView),
            new ObservableCollection<ResponseData>(),
            propertyChanged: OnMensListChanged
        );

    public static readonly BindableProperty WomenListProperty =
        BindableProperty.Create(
            nameof(WomenList),
            typeof(ObservableCollection<ResponseData>),
            typeof(CourseCardView),
            new ObservableCollection<ResponseData>(),
            propertyChanged: OnWomensListChanged
        );

    public static readonly BindableProperty IsloadingProperty =
   BindableProperty.Create(
       nameof(Isloading),
       typeof(bool),
       typeof(CourseCardView),
       true,
       propertyChanged: OnLoadingChanged
   );

    private static void OnLoadingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CourseCardView)bindable;
        MainThread.InvokeOnMainThreadAsync(() =>
        {
            control.myLoading.IsVisibleExternally = (bool)newValue; // بروزرسانی لیست جاری با توجه به تب فعال
        });
    }

    private static void OnWomensListChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CourseCardView)bindable;
        if (newValue != null && newValue is ObservableCollection<ResponseData> responseDatas)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                control.CurrentList.womens = responseDatas; // بروزرسانی لیست جاری با توجه به تب فعال
                control.OnPropertyChanged(nameof(CurrentList));
                control.OnPropertyChanged(nameof(BadgeCounts));

            });
        }
    }

    private static void OnMensListChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CourseCardView)bindable;
        if (newValue != null && newValue is ObservableCollection<ResponseData> responseDatas)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                control.CurrentList.mens = responseDatas; // بروزرسانی لیست جاری با توجه به تب فعال
                control.OnPropertyChanged(nameof(CurrentList));
                control.OnPropertyChanged(nameof(BadgeCounts));
            });
        }
    }

    private static void OnSharedListChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CourseCardView)bindable;
 
        if (newValue != null && newValue is ObservableCollection<ResponseData> responseDatas)
        {
   
            MainThread.BeginInvokeOnMainThread(() =>
            {
                control.CurrentList.shared = responseDatas;
                control.OnPropertyChanged(nameof(CurrentList));
                control.OnPropertyChanged(nameof(BadgeCounts));
            });
        }
        // بروزرسانی لیست جاری با توجه به تب فعال
    }



    public ObservableCollection<ResponseData> SharedList
    {
        get => (ObservableCollection<ResponseData>)GetValue(SharedListProperty);
        set => SetValue(SharedListProperty, value);
    }


    public ObservableCollection<ResponseData> MenList
    {
        get => (ObservableCollection<ResponseData>)GetValue(MenListProperty);
        set => SetValue(MenListProperty, value);
    }

    public ObservableCollection<ResponseData> WomenList
    {
        get => (ObservableCollection<ResponseData>)GetValue(WomenListProperty);
        set => SetValue(WomenListProperty, value);
    }

    public CourseCardView()
    {
        InitializeComponent();
        SelectedTabIndex = 0;
        UpdateCurrentList();

        SelectTabCommand = new Command<int>(OnSelectTab);
        RegisterCommand = new Command<ResponseData>(OnRegister);
        MemberListCommand = new Command<ResponseData>(OnMemberList);
    }

    private bool _isLoading = true;
    public bool Isloading
    {
        get => (bool)GetValue(IsloadingProperty);
        set => SetValue(IsloadingProperty, value);
    }

    public record ListsData()
    {
        public ObservableCollection<ResponseData> shared { get; set; } = new();
        public ObservableCollection<ResponseData> mens { get; set; } = new();
        public ObservableCollection<ResponseData> womens { get; set; } = new();
    }

    public static readonly BindableProperty CurrentListProperty =
    BindableProperty.Create(
        nameof(CurrentList),
        typeof(ListsData),
        typeof(CourseCardView),
        new ListsData(),
        propertyChanged: OnCurrentListPropertyChanged
    );

    private ListsData _currentList = new ListsData();
    public ListsData CurrentList
    {
        get => (ListsData)GetValue(CurrentListProperty);
        set
        {
            SetValue(CurrentListProperty, value);
            OnPropertyChanged(nameof(CurrentList));
        }
    }

    private static void OnCurrentListPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CourseCardView)bindable;
        MainThread.InvokeOnMainThreadAsync(() =>
        {
           
        });
    }
    //public ListsData CurrentList
    //{
    //    get => _currentList;
    //    set
    //    {
    //        _currentList = value;
    //        OnPropertyChanged(nameof(CurrentList));
    //    }
    //}

    private int _selectedTabIndex;
    public int SelectedTabIndex
    {
        get => _selectedTabIndex;
        set
        {
            if (_selectedTabIndex != value)
            {
                _selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex));
                UpdateCurrentList();
            }
        }
    }

    // تعداد آیتم‌ها برای Badge
    public (int Shared, int Men, int Women) BadgeCounts =>
        (SharedList.Count, MenList.Count, WomenList.Count);

    public ICommand SelectTabCommand { get; }
    public ICommand RegisterCommand { get; }
    public ICommand MemberListCommand { get; }

    private void OnSelectTab(int index)
    {
        SelectedTabIndex = index;
    }
    private void UpdateCurrentList()
    {
        switch (SelectedTabIndex)
        {
            case 0:
                SlideToTab(0);
                if (CurrentList is null ||  CurrentList.shared.Count == 0)  NotFound.IsVisible=true;
                else  NotFound.IsVisible = false;
                break;
            case 1:
                SlideToTab(1);
                if (CurrentList is null ||  CurrentList.womens.Count == 0) NotFound.IsVisible=true;
                else NotFound.IsVisible = false;
                break;
            case 2:
                SlideToTab(2);
                if (CurrentList is null ||  CurrentList.mens.Count == 0) NotFound.IsVisible=true;
                else NotFound.IsVisible = false;
                break;
        }

        OnPropertyChanged(nameof(BadgeCounts));
    }

    private async void OnRegister(ResponseData data)
    {
        try
        {
            await AppShell.Current.GoToAsync($"///CourseRegistery?CourseId={data.Id}");
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    private void OnMemberList(ResponseData data)
    {
        // اینجا می‌تونی لاجیک نمایش لیست اعضا رو اضافه کنی
        Application.Current.MainPage.DisplayAlert("اعضا", $"نمایش اعضای کلاس: {data.ClassName}", "باشه");
    }
    private void TapGestureShared_Tapped(object sender, TappedEventArgs e)
    {
        SelectedTabIndex = 0;
    }

    private void TapGestureMens_Tapped(object sender, TappedEventArgs e)
    {
        SelectedTabIndex = 1;
    }

    private void TapGestureWomens_Tapped(object sender, TappedEventArgs e)
    {
        SelectedTabIndex = 2;
    }

    private double _panStartX;
    private double _panCurrentX;
    private double _panDeltaX;
    private bool _isHorizontalPan;
    private int _currentIndex = 0;
    private const double SwipeThreshold = 20; // حداقل مسافت برای تشخیص سوایپ

    void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {

        switch (e.StatusType)
        {
            case GestureStatus.Started:
                _isHorizontalPan = true;
                _panStartX = CardGridParents.TranslationX;
                _panDeltaX = 0;
                break;

            case GestureStatus.Running:
                if (_isHorizontalPan)
                {
                    _panCurrentX = _panStartX + e.TotalX;
                    _panDeltaX = e.TotalX;
                }
                break;

            case GestureStatus.Completed:
                if (_isHorizontalPan)
                {
                    try
                    {
                        if (_panDeltaX > SwipeThreshold && _currentIndex < 2)
                            _currentIndex++;
                        else if (_panDeltaX < -SwipeThreshold && _currentIndex > 0)
                            _currentIndex--;
                        SelectedTabIndex = _currentIndex;
                        SlideToTab(SelectedTabIndex);

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    finally
                    {
                        _isHorizontalPan = false;
                    }
                }
                break;
        }
    }

    private async void SlideToTab(int tabIndex)
    {
        double w = CardGridParents.Width;
        if (w == 0)
        {
            await Task.Delay(50);
            w = CardGridParents.Width;
            if (w == 0) return; // باز هم مقدار نداده؟ بگذار به حال خودش.
        }

        //SelectedTabIndex = tabIndex;
        //_currentIndex = tabIndex;

        double targetX = 379 * tabIndex;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await CardGridParents.TranslateTo(targetX, 0, 300, Easing.CubicInOut);
        });
    }


    private bool _turnVisible = false;
    public bool TurnVisible
    {
        get => _turnVisible;
        set
        {
            _turnVisible = value;
            OnPropertyChanged(nameof(TurnVisible));
        }
    }

    public void ChnageTurn(bool TurnVisibleValue)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            TurnVisible = TurnVisibleValue;
        });
    }

    public void OnTryAgainClick(object sender, EventArgs e)
    {
        ChnageTurn(false);
    }
}
