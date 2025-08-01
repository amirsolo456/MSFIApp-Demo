using MSFIApp.Dtos.Public.Favorites;
using MSFIApp.Services.Common;
using System.Collections.ObjectModel;


namespace MSFIApp.Services.Public.Favorites
{
    public class FavoritesService : IFavoritesService<MSFIApp.Dtos.Public.Favorites.Response, ObservableCollection<ResponseData>, MSFIApp.Dtos.Public.Favorites.Request>
    {
        private readonly IApiCleint _apiClient;

        public FavoritesService(IApiCleint apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<ObservableCollection<ResponseData>>> GetFavoritesData(Request request)
        {
            var a = await _apiClient.ApiSendRequest<Response, ObservableCollection<ResponseData>, Request>("Public.Favorites.Favorites.json");
            return a;
        }
    }

    public interface IFavoritesService<T, C,D>
    {
        Task<ApiResponse<ObservableCollection<ResponseData>>> GetFavoritesData(D request);
    }
}
