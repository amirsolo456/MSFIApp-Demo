using MSFIApp.Dtos.User.ConfirmationCode;
using MSFIApp.Services.Common;

namespace MSFIApp.Services.User.ConfirmationCode
{
    public class ConfirmationCodeService : IConfirmationCodeService<Response, ResponseData, Request>
    {
        private readonly IApiCleint _apiClient;
        private readonly ISecureStorageService _secureStorageService;
        public ConfirmationCodeService(IApiCleint apiClient, ISecureStorageService secureStorageService)
        {
            _apiClient = apiClient;
            _secureStorageService = secureStorageService;
        }

        public async Task<ApiResponse<ResponseData>> CheckConfirmationCode(Request request)
        {
            try
            {
                var response = await _apiClient.ApiSendRequest<Response, ResponseData,Request>("User.ConfirmationCode.ConfirmationCode.json");

                if (response?.Entity != null)
                {
                    return response;
                }

                return new Response();
            }
            catch (Exception ex)
            {
                return new Response();
            }

        }
    }

    public interface IConfirmationCodeService<T,C, D>
    {
        Task<ApiResponse<ResponseData>> CheckConfirmationCode(D request);
    }
}
