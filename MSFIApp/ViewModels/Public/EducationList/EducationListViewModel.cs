using MSFIApp.Dtos.Public.EducationList;
using MSFIApp.Services.Common;
using MSFIApp.Services.Public.EducationList;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.ViewModels.Public.EducationList
{
    public class EducationListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        IEducationListService<Response, List<ResponseData>, Request> _EducationListService;
        public EducationListViewModel(IEducationListService<Response, List<ResponseData>, Request> educationListService)
        {
            _EducationListService = educationListService;
        }


        public async Task<ApiResponse<List<ResponseData>>> GetEducations(int ID)
        {

            var EduListResponse = await _EducationListService.GetEducationList(new Dtos.Public.EducationList.Request() { Id = ID });

            return EduListResponse;

        }
    }
}
