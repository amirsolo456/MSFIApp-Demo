using MSFIApp.Dtos.User.News;

namespace MSFIApp.Components.Controls;

public partial class DashboardView : ContentView
{

    public DashboardView()
    {
        InitializeComponent();

        if (Response != null)
            collectionView.ItemsSource = Response?.Entity?.Articles;
    }

    public static readonly BindableProperty ResponseProperty =
           BindableProperty.Create(nameof(Response), typeof(Response), typeof(DashboardView));

    public Response Response
    {
        get => (Response)GetValue(ResponseProperty);
        set => SetValue(ResponseProperty, value);
    }

}