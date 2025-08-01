using MSFIApp.Services.Public.Favorites;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.ViewModels.Public.Favorites
{
    public class FavoritesViewModel : INotifyPropertyChanged
    {
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<MSFIApp.Dtos.Public.Favorites.ResponseData> Response { get; set; } = new();
        public MSFIApp.Dtos.Public.Favorites.Request Request { get; set; } = new();

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IFavoritesService<MSFIApp.Dtos.Public.Favorites.Response, ObservableCollection<MSFIApp.Dtos.Public.Favorites.ResponseData>, MSFIApp.Dtos.Public.Favorites.Request> _favoritesService;

        public async Task LoadDataAsync()
        {
            Request = new MSFIApp.Dtos.Public.Favorites.Request()
            {

            };
            var resp = await _favoritesService?.GetFavoritesData(Request);
            foreach (var item in resp.Entity)
            {
                Response.Add(item);
            } 

            OnPropertyChanged();
        }

        public FavoritesViewModel()
        { }

        public FavoritesViewModel(IFavoritesService<MSFIApp.Dtos.Public.Favorites.Response, ObservableCollection<MSFIApp.Dtos.Public.Favorites.ResponseData>, MSFIApp.Dtos.Public.Favorites.Request> favoritesService)
        {
            _favoritesService = favoritesService;
            LoadDataAsync();
        }
    }
}
