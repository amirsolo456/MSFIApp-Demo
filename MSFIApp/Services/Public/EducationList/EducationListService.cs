using MSFIApp.Dtos.Public.EducationList;
using MSFIApp.Services.Common;

namespace MSFIApp.Services.Public.EducationList
{
    public class EducationListService : IEducationListService<Response, List<ResponseData>, Request>
    {
        private readonly IApiCleint _apiClient;

        //private readonly ISecureStorageService _secureStorageService;
        public EducationListService(IApiCleint apiClient)
        {
            _apiClient = apiClient;
            //_secureStorageService = secureStorageService;
        }

        public async Task<ApiResponse<List<ResponseData>>> GetEducationList(Request request)
        {
            try
            {
                var response = await _apiClient.ApiSendRequest<Response, List<ResponseData>, Request>("Public.EducationList.EducationList.json");
                return response;
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ResponseData>>()
                {
                    Error = new ApiError()
                    {
                        Code = ex.Message,
                        Message = ex.Message,
                    }
                };
            }
        }
    }

    public interface IEducationListService<T,C,D>
    {
        Task<ApiResponse<List<ResponseData>>> GetEducationList(D request);
    }
}
