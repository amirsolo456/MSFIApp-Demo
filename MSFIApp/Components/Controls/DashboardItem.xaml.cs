using MSFIApp.Dtos.User.News;
namespace MSFIApp.Components.Controls;


public partial class DashboardItem : ContentView
{
    public DashboardItem()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty ArticleProperty =
       BindableProperty.Create(nameof(Article), typeof(ArticleModel), typeof(DashboardItem), propertyChanged: OnArticleChanged);

    public ArticleModel Article
    {
        get => (ArticleModel)GetValue(ArticleProperty);
        set => SetValue(ArticleProperty, value);
    }

    private static void OnArticleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        // اینجا اگه بخوای کاری بکنی هنگام تغییر مقاله می‌تونی انجام بدی
    }
}