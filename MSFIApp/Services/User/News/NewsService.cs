using MSFIApp.Dtos.User.News;
using MSFIApp.Services.Common;

namespace MSFIApp.Services.Main
{
    public class NewsService : INewsService<MSFIApp.Dtos.User.News.Response, MSFIApp.Dtos.User.News.ResponseData, MSFIApp.Dtos.User.News.Request>
    {
        private readonly IApiCleint _apiClient;

        public NewsService(IApiCleint apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<MSFIApp.Dtos.User.News.ResponseData>> GetNewsData(Request request)
        {
            var a = await _apiClient.ApiSendRequest<Response, ResponseData, Request>("User.News.News.json");
            return a;
        }
    }

    public interface INewsService<T,C, D>
    {
        Task<ApiResponse<MSFIApp.Dtos.User.News.ResponseData>> GetNewsData(D request);
    }
}
