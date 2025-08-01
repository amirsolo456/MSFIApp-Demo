using MSFIApp.Dtos.User.UserMenu;
using MSFIApp.Services.Common;

namespace MSFIApp.Services.User.UserMenu
{
    public class UserMenuService : IUserMenuService<Response, List<ResponseData>, Request>
    {
        private readonly IApiCleint _apiClient;
        public UserMenuService(IApiCleint apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<List<ResponseData>>> GetUserMenuData(Request request)
        {
            var a = await _apiClient.ApiSendRequest<Response, List<ResponseData>, Request>("User.UserMenu.UserMenu.json");
            return a;
        }
    }

    public interface IUserMenuService<T,C, D>
    {
        Task<ApiResponse<List<ResponseData>>> GetUserMenuData(D request);
    }
}
