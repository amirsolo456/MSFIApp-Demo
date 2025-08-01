using MSFIApp.Services.Common;
using System.Collections.ObjectModel;

namespace MSFIApp.Dtos.Public.Favorites
{
    public class Request
    {
    }

    public class Response : ApiResponse<ObservableCollection<ResponseData>>
    {
    }


    public class ResponseData
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public bool? IsHave { get; set; }
    }
}
