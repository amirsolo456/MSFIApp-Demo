namespace MSFIApp.Components.Objects;

public partial class MainHeader : ContentView
{
    public event EventHandler MorevetClick;
    public event EventHandler LogoClick;
    public event EventHandler FoureVetClick;
    public MainHeader()
    {
        InitializeComponent();
    }

    private void morevet_Clicked(object sender, EventArgs e)
    {
        MorevetClick?.Invoke(sender, e);
    }

    private void msflogo_Clicked(object sender, EventArgs e)
    {
        LogoClick?.Invoke(sender, e);
    }

    private void fourevet_Clicked(object sender, EventArgs e)
    {
        FoureVetClick?.Invoke(sender, e);
    }
}