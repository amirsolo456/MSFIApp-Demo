using MSFIApp.Dtos.BaseData.Areas;
using MSFIApp.ViewModels.BaseData.Cities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.Components.Controls;

public partial class CitiesSelectBox : ContentView, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private CitiesViewModel _CitiesViewModel;

    public int PageSize = 10000;
    public CitiesSelectBox()
    {
        InitializeComponent();
        Loaded += CitiesSelectBox_Loaded;
    }

    private async void CitiesSelectBox_Loaded(object? sender, EventArgs e)
    {
        Loaded -= CitiesSelectBox_Loaded;
        _CitiesViewModel = App.Current?.Handler?.MauiContext?.Services?.GetService<CitiesViewModel>();
    }

    public static readonly BindableProperty ItemsSourceProperty =
    BindableProperty.Create(
        nameof(ItemsSource),
        typeof(ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>),
        typeof(CitiesSelectBox),
        defaultValue: new ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>());

    public ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData> ItemsSource
    {
        get => (ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>)GetValue(ItemsSourceProperty);
        set
        {
            SetValue(ItemsSourceProperty, value);
            OnPropertyChanged(nameof(ItemsSource));
        }
    }

    public static readonly BindableProperty AreaIDProperty =
        BindableProperty.Create(
            nameof(AreaID),
            typeof(int),
            typeof(CitiesSelectBox),
            defaultValue: 0,
            propertyChanged: OnAreaIDChanged);

    public int AreaID
    {
        get => (int)GetValue(AreaIDProperty);
        set => SetValue(AreaIDProperty, value);
    }

    private static void OnAreaIDChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CitiesSelectBox)bindable;
        if (newValue != null && newValue is int val)
        {
            control.GetCities(val);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                control.SearchText = string.Empty;
                control.SelectedItem = new ResponseData();
            });
        }
    }

    public async void GetCities(int areaid)
    {
        try
        {
            if (areaid != 0)
            {
                var data = await _CitiesViewModel.GetAllCities(PageSize, areaid);
                ItemsSource = data;
                FilteredAreas = ItemsSource;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


    public static readonly BindableProperty TitleProperty =
       BindableProperty.Create(nameof(Title), typeof(string), typeof(CitiesSelectBox), defaultValue: default(string));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set
        {
            SetValue(TitleProperty, value);
            OnPropertyChanged(nameof(Title));
        }
    }


    public static readonly BindableProperty SelectedItemProperty =
       BindableProperty.Create(nameof(SelectedItem), typeof(MSFIApp.Dtos.BaseData.Areas.ResponseData), typeof(CitiesSelectBox), defaultValue: new MSFIApp.Dtos.BaseData.Areas.ResponseData(), BindingMode.TwoWay);

    public MSFIApp.Dtos.BaseData.Areas.ResponseData SelectedItem
    {
        get => (MSFIApp.Dtos.BaseData.Areas.ResponseData)GetValue(SelectedItemProperty);
        set
        {
            SetValue(SelectedItemProperty, value);
            OnPropertyChanged(nameof(SelectedItem));
        }
    }

    public static readonly BindableProperty SelectedItemTextProperty =
        BindableProperty.Create(nameof(SearchText), typeof(string), typeof(CitiesSelectBox), defaultValue: default(string), propertyChanged: OnSelectedItemTextPropertyChanged);

    public string SearchText
    {
        get => (string)GetValue(SelectedItemTextProperty);
        set
        {
            IsDeletehMode = (!string.IsNullOrEmpty(value) ? true : false);
            SetValue(SelectedItemTextProperty, value);
            OnPropertyChanged(nameof(SearchText));
        }
    }

    private static void OnSelectedItemTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CitiesSelectBox)bindable;
        if (newValue != null && newValue is string val && !string.IsNullOrEmpty(val))
        {
            control.FilterItems(val);
        }
        else
        {
             control.FilterItems(null);
        }
    }

    private bool _isexpanded = false;
    public bool _isExpanded
    {
        get => _isexpanded;
        set
        {
            _isexpanded = value;
            IsDeletehMode = value;
        }
    }

    private async void OnTapped(object sender, EventArgs e)
    {
        _isExpanded = !_isExpanded;
        if (AreaPopupControl != null)
        {
            AreaPopupControl.Show();
            OnPropertyChanged(nameof(AreaPopupControl));
        }
    }

    private void OnSelectItem(object sender, MSFIApp.Dtos.BaseData.Areas.ResponseData Item)
    {
        try
        {
            SelectedItem = Item;           
             _isExpanded= false;
            IsSearchMode = false;
            SearchText = Item?.Title;
        }
        catch (Exception)
        {

            throw;
        }
    }

    private bool _isDeleteMode = false;
    public bool IsDeletehMode
    {
        get => _isDeleteMode;
        set
        {
            bool result = false;
            if (string.IsNullOrEmpty(SearchText)) result = false;
            else
            {
                result = true;
                if (_isExpanded == false) result = false;
                else result = true;
            }

            if (IsDeletehMode != result)
            {
                _isDeleteMode = result;
                OnPropertyChanged(nameof(IsDeletehMode));
            }
        }
    }

    private bool _isSearchMode;
    public bool IsSearchMode
    {
        get => _isSearchMode;
        set
        {
            _isSearchMode = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IconSource)); // برای تغییر آیکون
        }
    }

    public string IconSource => IsSearchMode ? "search.png" : "arrow_down.png";

    private ObservableCollection<ResponseData> _filteredAreas = new();
    public ObservableCollection<ResponseData> FilteredAreas
    {
        get => _filteredAreas;
        set
        {
            _filteredAreas = value;
            OnPropertyChanged(nameof(FilteredAreas));
        }
    }

    private void FilterItems(string txt = null)
    {
        if (IsSearchMode)
        {
            string TextString = SearchText;
            if (!string.IsNullOrEmpty(txt)) TextString = txt;

            if (string.IsNullOrWhiteSpace(TextString))
            {
                FilteredAreas = ItemsSource;
            }
            else
            {
                var filtered = ItemsSource
                    ?.Where(x => x.Title?.Contains(TextString, StringComparison.OrdinalIgnoreCase) == true)
                    ?.ToList();
                FilteredAreas = new ObservableCollection<ResponseData>(filtered);
            }
        }
    }

    private void OnIconClicked(object sender, EventArgs e)
    {
        try
        {
            IsSearchMode = !IsSearchMode;
            if (IsSearchMode)
            {
                if (FilteredAreas != null && FilteredAreas.Count > 0)
                {
                    AreaPopupControl.Show();
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        SearchEntry.Focus();
                    });
                    _isExpanded = true;
                }
                else IsSearchMode = false;
            }
            else
            {
                AreaPopupControl.Hide();
                _isExpanded = false;
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

    private void OnClearClicked(object sender, EventArgs e)
    {
        SearchText = string.Empty;
        FilteredAreas = ItemsSource;
        IsSearchMode = false;
        AreaPopupControl.Hide();
    }

    private void SearchEntry_Unfocused(object sender, FocusEventArgs e)
    {
        //SelectorBorder.Stroke = Colors.Black;
    }

    private void SearchEntry_OnFocused_1(object sender, EventArgs e)
    {
        OnIconClicked(sender, e);
    }
}