using MSFIApp.Dtos.Autorize.Login;
using MSFIApp.Services.Common;

namespace MSFIApp.Services.Autorize
{
    public class LoginService : ILoginService<Response,ResponseData, Request>
    {
        private readonly IApiCleint _apiClient;
        private readonly ISecureStorageService _secureStorageService;

        public LoginService(IApiCleint apiClient,ISecureStorageService secureStorageService)
        {
            _apiClient = apiClient;
            _secureStorageService = secureStorageService;
        }


        public async Task<ApiResponse<ResponseData>> Authenticate(Request request, bool SaveUser = false)
        {
            var response = await _apiClient.ApiSendRequest<Response, ResponseData, Request>("Authorize.Login.Authorize.json");
            try
            {
                return response;
            }
            catch (Exception ex)
            {
                return new Response();
            }
            finally
            {
                if (SaveUser)
                {
                    _secureStorageService.SaveUser("user", response.Entity);
                }
            }
        }



        public async Task<ApiResponse<ResponseData>> AuthenticateV1(Request request, bool SaveUser = false)
        {
            ApiResponse<ResponseData> response = null;
            try
            {
                var b = await _apiClient.ApiSendRequest<Response,ResponseData,Request>("Authorize.Login.Authorize.json");
                return b;
            }
            catch (Exception ex)
            {
                return new  Response()
                {
                    Error = new ApiError
                    {
                        Message = ex.Message,
                        Code = ex.Message
                        
                    }
                };
            }
            finally
            {

                if (SaveUser)
                {
                    _secureStorageService.SaveUser("user", response.Entity);
                }

            }

            return null;
        }
    }

    public interface ILoginService<T,C,D>
    {
        Task<ApiResponse<ResponseData>> Authenticate(D request, bool SaveUser = false);
        Task<ApiResponse<ResponseData>> AuthenticateV1(D request, bool SaveUser = false);
    }
}
