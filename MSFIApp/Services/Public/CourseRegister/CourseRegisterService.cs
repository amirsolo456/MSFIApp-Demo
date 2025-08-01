using MSFIApp.Dtos.Public.CourseRegister;
using MSFIApp.Services.Common;

namespace MSFIApp.Services.Public.CourseRegister
{

    public class CourseRegisterService : ICourseRegisterService<Response,   ResponseData, Request>
    {
        private readonly IApiCleint _apiClient;

        public CourseRegisterService(IApiCleint apiClient)
        {
            _apiClient = apiClient;

        }

        public async Task<ApiResponse<ResponseData>> GetCourseRegisteryDatas(MSFIApp.Dtos.Public.CourseRegister.Request request)
        {
            try
            {
                var response = await _apiClient.ApiSendRequest<Response, ResponseData, Request>("Public.CourseRegister.CourseRegister.json");

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

    public interface ICourseRegisterService<T,C, D>
    {
        Task<ApiResponse<ResponseData>> GetCourseRegisteryDatas(D request);
    }
}
