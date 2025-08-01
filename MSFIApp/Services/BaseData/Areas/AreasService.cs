using MSFIApp.Dtos.BaseData.Areas;
using MSFIApp.Services.Common;

namespace MSFIApp.Services.BaseData.Areas
{

    public class AreasService : IAreasService<Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>, Request>
    {
        private readonly IApiCleint _apiClient;


        public AreasService(IApiCleint apiClient)
        {
            _apiClient = apiClient;

        }

        public async Task<ApiResponse<List<MSFIApp.Dtos.BaseData.Areas.ResponseData>>> GetAreas(MSFIApp.Dtos.BaseData.Areas.Request request)
        {
            try
            {
                var response = await _apiClient.ApiSendRequest<Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>, MSFIApp.Dtos.BaseData.Areas.Request>("BaseData.Areas.Areas.json");

                if (response?.Entity != null)
                {
                    return response;
                }

                return new ApiResponse<List<MSFIApp.Dtos.BaseData.Areas.ResponseData>>();
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<MSFIApp.Dtos.BaseData.Areas.ResponseData>>();
            }

        }
    }

    public interface IAreasService<T,C, D>
    {
        Task<ApiResponse<List<MSFIApp.Dtos.BaseData.Areas.ResponseData>>> GetAreas(D request);
    }
}
