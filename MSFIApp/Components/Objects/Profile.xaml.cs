namespace MSFIApp.Components.Objects;

public partial class Profile : ContentView
{
    Microsoft.Maui.Graphics.Color color = Color.FromRgba(22, 119, 255, 1);
    public Profile()
    {
        InitializeComponent();
    }

    private void IconButton_Clicked(object sender, EventArgs e)
    {

    }

    private async void SignOut_Clicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync("///Login");
    }
}