using MSFIApp.Dtos.BaseData.Cities;
using MSFIApp.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSFIApp.Services.BaseData.Cities
{
    public class CitiesService : ICitiesService<Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>, Request>
    {
        private readonly IApiCleint _apiClient;


        public CitiesService(IApiCleint apiClient)
        {
            _apiClient = apiClient;

        }

        public async Task<ApiResponse<List<MSFIApp.Dtos.BaseData.Areas.ResponseData>>> GetCities(MSFIApp.Dtos.BaseData.Cities.Request request)
        {
            try
            {
                var response = await _apiClient.ApiSendRequest<Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>, MSFIApp.Dtos.BaseData.Cities.Request>("BaseData.Cities.Cities.json");

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

    public interface ICitiesService<T,C, D>
    {
        Task<ApiResponse<List<MSFIApp.Dtos.BaseData.Areas.ResponseData>>> GetCities(D request);
    }
}
