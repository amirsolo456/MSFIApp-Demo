using MSFIApp.Dtos.Common;
using MSFIApp.Services.Common;
using MSFIApp.Services.Public.CourseRegister;

namespace MSFIApp.ViewModels.Public.CourseRegister
{

    public class CourseRegisterViewModel : BaseViewModel
    {
        int Duration = 300;

        ICourseRegisterService<MSFIApp.Dtos.Public.CourseRegister.Response, MSFIApp.Dtos.Public.CourseRegister.ResponseData,MSFIApp.Dtos.Public.CourseRegister.Request> _CoursesRegService;

        public CourseRegisterViewModel(ICourseRegisterService<MSFIApp.Dtos.Public.CourseRegister.Response, MSFIApp.Dtos.Public.CourseRegister.ResponseData,MSFIApp.Dtos.Public.CourseRegister.Request> CoursesregService, IMessageService messageService) : base(messageService)
        {
            _CoursesRegService = CoursesregService;

        }

        public async Task DoSomething(Exception ex, int duration = 300, string Context = null)
        {
            await ShowMessageAsync(ex, duration, Context);
        }

        public async Task<ApiResponse<MSFIApp.Dtos.Public.CourseRegister.ResponseData>> GetCoursese(int ID)
        {
            try
            {
                var Coursesresponse = await _CoursesRegService.GetCourseRegisteryDatas(new MSFIApp.Dtos.Public.CourseRegister.Request() { id = ID });
                if (Coursesresponse != null && Coursesresponse.Entity != null)
                {
                    return Coursesresponse;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                await DoSomething(ex, Duration);
            }

            return new ApiResponse<MSFIApp.Dtos.Public.CourseRegister.ResponseData>();
        }
    }
}
