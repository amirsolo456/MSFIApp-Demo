using CommunityToolkit.Maui.Views;
using MSFIApp.Dtos.Common;

namespace MSFIApp.Components.Controls;

public partial class ExpansionPannel : ContentView
{
    public ExpansionPannel()
    {
        InitializeComponent();
        BindingContext = new MSFIApp.ViewModels.Common.ExpansionPannelViewModel();
    }

    private void MainExpander_ExpandedChanged(object sender, EventArgs e)
    {
        if (sender is Expander expander)
        {
            Caret.IsExpanded = expander.IsExpanded;
        }
    }

    public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
    nameof(IconSource),
    typeof(ImageSource),
    typeof(ExpansionPannel),
    default(ImageSource),
    propertyChanged: OnIconSourceChanged);

    public ImageSource IconSource
    {
        get => (ImageSource)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    private static void OnIconSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ExpansionPannel pannel && newValue is ImageSource icon)
        {
            pannel.MyIconButton.IconSource = icon;
        }
    }

    public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(
        nameof(HeaderText),
        typeof(string),
        typeof(ExpansionPannel),
        string.Empty,
        propertyChanged: OnHeaderTextChanged);

    public string HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    private static void OnHeaderTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ExpansionPannel pannel && newValue is string text)
        {
            pannel.MyIconButton.Text = text;
        }
    }

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
    nameof(Items),
    typeof(IEnumerable<ExpansionPannelRow>),
    typeof(ExpansionPannel),
    default(IEnumerable<ExpansionPannelRow>),
    propertyChanged: OnItemsChanged);

    public IEnumerable<ExpansionPannelRow> Items
    {
        get => (IEnumerable<ExpansionPannelRow>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    private static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ExpansionPannel pannel && newValue is IEnumerable<ExpansionPannelRow> items)
        {
            // ViewModel از قبل ست شده
            //if (pannel.BindingContext is ExpansionPannelViewModel vm)
            //{
            //    vm.Items = new ObservableCollection<ExpansionPannelRow>(items);
            //    vm.OnPropertyChanged(nameof(vm.Items));
            //}
        }
    }

    //public ICommand OnItemTappedCommand => new Command<object>(item =>
    //{
    //    if (item is MyItemModel m)
    //        Debug.WriteLine($"Item tapped: {m.Text}");
    //});

    //<controls:ExpansionPannel
    //Items = "{Binding MyItems}"
    //ItemTappedCommand="{Binding OnItemTappedCommand}" />

}