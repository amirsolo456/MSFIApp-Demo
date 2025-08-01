using MSFIApp.ViewModels.Public.Favorites;

namespace MSFIApp.Components.Objects;

public partial class Favorites : ContentView
{
    public Favorites(FavoritesViewModel favorites)
    {
        InitializeComponent();
        BindingContext = favorites;
    }

    public Favorites()
    {
        InitializeComponent();
    }
}