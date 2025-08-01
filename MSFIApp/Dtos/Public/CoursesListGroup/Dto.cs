using MSFIApp.Services.Common;
using System.Collections.ObjectModel;

namespace MSFIApp.Dtos.Public.CoursesListGroupService
{
    public class Request
    {
        public int ID { get; set; } = 1;
    }

    public class Response : ApiResponse<ObservableCollection<ResponseData>>
    {

    }

    public class ResponseData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DisplayTitle { get; set; }
        public string TitleEn { get; set; }
        public string AdditionalData { get; set; }
    }
}
