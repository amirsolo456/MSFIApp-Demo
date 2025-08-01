using MSFIApp.Dtos.User.SignUp;
using MSFIApp.Services.Common;

namespace MSFIApp.Services.User.SignUp
{
    public class SignUpService : ISignUpService<Response, ResponseData,Request>
    {
        private readonly IApiCleint _apiClient;

        public SignUpService(IApiCleint apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<ResponseData>> SignUpGetData(Request request)
        {
            ApiResponse<ResponseData> response = null;
            response = await _apiClient.ApiSendRequest<Response, ResponseData,Request>("User.SignUp.SignUp.json");
            return response;
        }


    }
    public interface ISignUpService<T,C, D>
    {
        Task<ApiResponse<ResponseData>> SignUpGetData(D request);
    }
}
