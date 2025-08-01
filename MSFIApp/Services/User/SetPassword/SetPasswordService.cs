using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSFIApp.Dtos.User.SetPassword;
using MSFIApp.Services.Common;

namespace MSFIApp.Services.User.SetPassword
{
    public class SetPasswordService : ISetPasswordService<MSFIApp.Dtos.User.SetPassword.Response, ResponseData, MSFIApp.Dtos.User.SetPassword.Request>
    {
        private readonly IApiCleint _apiClient;

        public SetPasswordService(IApiCleint apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<ResponseData>> SetPassword(Request request)
        {
            try
            {
                var response = await _apiClient.ApiSendRequest<Response, ResponseData, Request>("User.SetPassword.SetPassword.json");
                return response;
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Error = new ApiError
                    {
                        Message = ex.Message,
                        Code = ex.Message,
                    }
                };
            }
        }
    }

    public interface ISetPasswordService<T,C, D>
    {
        Task<ApiResponse<ResponseData>> SetPassword(D request);
    }
}
