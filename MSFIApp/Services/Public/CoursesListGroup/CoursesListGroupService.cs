using MSFIApp.Dtos.Public.CoursesListGroupService;
using MSFIApp.Services.Common;
using System.Collections.ObjectModel;

namespace MSFIApp.Services.Public.CoursesGroup
{
    public class CoursesListGroupService : ICoursesListGroupService<Response, ObservableCollection<ResponseData>, Request>
    {
        private readonly IApiCleint _apiClient;
        public CoursesListGroupService(IApiCleint apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<ObservableCollection<ResponseData>>> GetCoursesGroup(MSFIApp.Dtos.Public.CoursesListGroupService.Request request)
        {
            
            try
            {
                var response = await _apiClient.ApiSendRequest<Response, ObservableCollection<ResponseData>, Request>("Public.CoursesListGroup.CoursesGroup.json");
                return response;
            }
            catch (Exception ex)
            {
      
                return new  ApiResponse<ObservableCollection<ResponseData>>() { Error=new ApiError()
                {
                    Code= ex.Message,
                    Message=ex.Message,
                } };
            }
     
        }
    }

    public interface ICoursesListGroupService<T,C,D>
    {
        Task<ApiResponse<ObservableCollection<ResponseData>>> GetCoursesGroup(D request);
    }
}
