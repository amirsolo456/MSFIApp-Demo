using MSFIApp.Dtos.User.PasswordRecovery;
using MSFIApp.Services.Common;

namespace MSFIApp.Services.User.PasswordRecovery
{
    public class PasswordRecoveryService : IPasswordRecoveryService<Response, List<MSFIApp.Dtos.User.PasswordRecovery.ResponseData>, MSFIApp.Dtos.User.PasswordRecovery.Request>
    {
        private readonly IApiCleint _apiClient;

        public PasswordRecoveryService(IApiCleint apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<List<MSFIApp.Dtos.User.PasswordRecovery.ResponseData>>> GetPasswordRecoveryData(Request request)
        {
            try
            {
                var response = await _apiClient.ApiSendRequest<Response, List<MSFIApp.Dtos.User.PasswordRecovery.ResponseData>, Request>("User.PasswordRecovery.PasswordRecovery.json");
                return response;
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Error=new ApiError
                    {
                        Message = ex.Message,
                        Code = ex.Message,
                    }
                };
            }
        }
    }

    public interface IPasswordRecoveryService<T,C, D>
    {
        Task<ApiResponse<List<MSFIApp.Dtos.User.PasswordRecovery.ResponseData>>> GetPasswordRecoveryData(D request);
    }
}
