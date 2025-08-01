using MSFIApp.Dtos.BaseData.ClassRegisttration;
using MSFIApp.Services.Common;
using System.Collections.ObjectModel;

namespace MSFIApp.Services.BaseData.ClassRegistration
{
    public class ClassRegistrationService : IClassRegistrationService<Response, ObservableCollection<ResponseData>, Request>
    {
        private readonly IApiCleint _apiClient;

        private readonly ISecureStorageService _secureStorageService;
        public ClassRegistrationService(IApiCleint apiClient, ISecureStorageService secureStorageService)
        {
            _apiClient = apiClient;
            _secureStorageService = secureStorageService;
        }

        public async Task<ApiResponse<ObservableCollection<ResponseData>>> GetClassRegistrationData(Request request)
        {
            try
            {
                var response = await _apiClient.ApiSendRequest<Response, ObservableCollection<ResponseData>, Request>("BaseData.ClassRegistration.ClassRegistration.json");

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

    public interface IClassRegistrationService<T, C, D>
    {
        Task<ApiResponse<ObservableCollection<ResponseData>>> GetClassRegistrationData(D request);
    }
}
