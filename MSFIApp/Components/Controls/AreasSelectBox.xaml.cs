using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using MSFIApp.Dtos.BaseData.Areas;
using MSFIApp.ViewModels.BaseData.Areas;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.Components.Controls;

public partial class AreasSelectBox : ContentView, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private AreasViewModel _AreasViewModel;
    public int PageSize = 10000;
    public AreasSelectBox()
    {
        InitializeComponent();
        Loaded += AreasSelectBox_Loaded;
    }

    private async void AreasSelectBox_Loaded(object? sender, EventArgs e)
    {
        Loaded -= AreasSelectBox_Loaded;
        _AreasViewModel = App.Current?.Handler?.MauiContext?.Services?.GetService<AreasViewModel>();
 
        var data = await  _AreasViewModel.GetAllAreas(PageSize);
        ItemsSource = data;
        FilteredAreas = ItemsSource;
    }

    private void OnProvinceSelected(MSFIApp.Dtos.BaseData.Areas.ResponseData selected)
    {
        SelectedProvince = selected;
    }

    private MSFIApp.Dtos.BaseData.Areas.ResponseData _selectedProvince;
    public MSFIApp.Dtos.BaseData.Areas.ResponseData SelectedProvince
    {
        get => _selectedProvince;
        set
        {
            if (_selectedProvince != value)
            {
                _selectedProvince = value;
                OnPropertyChanged();
            }
        }
    }


    private bool _isExpanded = false;

    #region Bindable Properties

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(AreasSelectBox), defaultValue: default(string));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set
        {
            SetValue(TitleProperty, value);
            OnPropertyChanged(nameof(Title));
        }
    }

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(
            nameof(ItemsSource),
            typeof(ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>),
            typeof(AreasSelectBox),
            new ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>(),
            propertyChanged: OnItemsSourceChanged);

    public ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData> ItemsSource
    {
        get => (ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AreasSelectBox)bindable;
        if(control.AreaPopupControl != null)
            control.AreaPopupControl.Areas = (ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>)newValue;
    }

    public static readonly BindableProperty SelectedItemProperty =
        BindableProperty.Create(nameof(SelectedItem), typeof(MSFIApp.Dtos.BaseData.Areas.ResponseData), typeof(AreasSelectBox), defaultValue: new MSFIApp.Dtos.BaseData.Areas.ResponseData(), BindingMode.TwoWay);

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
         BindableProperty.Create(nameof(SearchText), typeof(string), typeof(AreasSelectBox), default(string), propertyChanged: OnSelectedItemTextPropertyChanged);

    public string SearchText
    {
        get => (string)GetValue(SelectedItemTextProperty);
        set
        {
            SetValue(SelectedItemTextProperty, value);
            OnPropertyChanged(nameof(SearchText));
        }
    }

    private static void OnSelectedItemTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AreasSelectBox)bindable;
        if (newValue != null && newValue is string val && !string.IsNullOrEmpty(val))
        {
            //control.SelectorBorder.Opacity = 1;
            control.FilterItems(val);
        }
        else
        {
            //control.SelectorBorder.Opacity = 0.25;
            control.FilterItems(null);
            control.IsDeletehMode = false;
        }
    }

    #endregion

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
            IsSearchMode = false;
            SelectedItem = Item;
            SearchText = Item?.Title;
            _isExpanded = IsSearchMode = false;
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
            _isDeleteMode = value;
            OnPropertyChanged(nameof(IsDeletehMode)); // برای تغییر آیکون
        }
    }


    private bool _isSearchMode = false;
    public bool IsSearchMode
    {
        get => _isSearchMode;
        set
        {
            _isSearchMode = value;
            if (!string.IsNullOrEmpty(SearchText)) IsDeletehMode = value;
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
            if (!string.IsNullOrEmpty(txt)) TextString = SearchText;

            if (string.IsNullOrWhiteSpace(TextString))
            {
                FilteredAreas = new ObservableCollection<ResponseData>(ItemsSource);
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
        IsSearchMode = !IsSearchMode;

        if (IsSearchMode)
        {
            if (FilteredAreas != null && FilteredAreas.Count > 0)
            {
                AreaPopupControl.Show();
                _isExpanded = true;
            }
            else IsSearchMode = false;

        }
        else
        {
            // بستن حالت سرچ
            //SearchText = string.Empty;
            AreaPopupControl.Hide();
            _isExpanded = false;
        }
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        SearchText = string.Empty;
        FilteredAreas = ItemsSource;
        IsSearchMode = false;
        AreaPopupControl.Hide();
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (IsSearchMode)
        {
            FilterItems(SearchText);
        }
        if (!string.IsNullOrEmpty(SearchText)) IsDeletehMode = true;
    }

    private void SearchEntry_OnFocused(object sender, EventArgs e)
    {
        OnIconClicked(sender, e);
    }
}
