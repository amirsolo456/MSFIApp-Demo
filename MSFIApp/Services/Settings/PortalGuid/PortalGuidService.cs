using MSFIApp.Dtos.Settings.PortalGuid;
using MSFIApp.Services.Common;

namespace MSFIApp.Services.PortalGuid
{
    public class PortalGuidService : IPortalGuidService<Response, List<ResponseData>, MSFIApp.Dtos.Settings.PortalGuid.Request>
    {
        private readonly IApiCleint _apiClient;


        public PortalGuidService(IApiCleint apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<List<ResponseData>>> GetPortablData(Request request)
        {
            try
            {
                var response = await _apiClient?.ApiSendRequest<Response, List<ResponseData>, Request>("PortalGuid.json");
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

    public interface IPortalGuidService<T,C, D>
    {
        Task<ApiResponse<List<ResponseData>>> GetPortablData(D request);
    }
}
