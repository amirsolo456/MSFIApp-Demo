using MSFIApp.Dtos.Common;
using MSFIApp.Services.Common;
using MSFIApp.Services.Public.CoursesGroup;
using System.Collections.ObjectModel;

namespace MSFIApp.ViewModels.Public.CoursesGroup
{
    public class CoursesListGroupViewModel : BaseViewModel
    {
        int Duration = 300;

        ICoursesListGroupService<MSFIApp.Dtos.Public.CoursesListGroupService.Response, ObservableCollection<MSFIApp.Dtos.Public.CoursesListGroupService.ResponseData>,MSFIApp.Dtos.Public.CoursesListGroupService.Request> _CoursesGroupService;

        public CoursesListGroupViewModel(ICoursesListGroupService<MSFIApp.Dtos.Public.CoursesListGroupService.Response, ObservableCollection<MSFIApp.Dtos.Public.CoursesListGroupService.ResponseData>, MSFIApp.Dtos.Public.CoursesListGroupService.Request> CoursesGroupService, IMessageService messageService) : base(messageService)
        {
            _CoursesGroupService = CoursesGroupService;

        }

        public async Task DoSomething(Exception ex, int duration = 300, string Context = null)
        {
            await ShowMessageAsync(ex, duration, Context);
        }

        public async Task<ApiResponse<ObservableCollection<MSFIApp.Dtos.Public.CoursesListGroupService.ResponseData>>> GetCoursese(int ID)
        {

            var Coursesresponse = await _CoursesGroupService?.GetCoursesGroup(new MSFIApp.Dtos.Public.CoursesListGroupService.Request());
            return Coursesresponse;

        }
    }
}
