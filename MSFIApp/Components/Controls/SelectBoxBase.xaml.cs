namespace MSFIApp.Components.Controls;

public partial class SelectBoxBase : ContentView
{
	public SelectBoxBase()
	{
		InitializeComponent();
    }

    public VerticalStackLayout GetContentLayout() => ContentLayout;
    public View GetContent() => Content;

}