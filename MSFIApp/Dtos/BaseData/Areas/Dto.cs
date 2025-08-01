using MSFIApp.Services.Common;

namespace MSFIApp.Dtos.BaseData.Areas
{
    public class Request
    {
        public int PageSize { get; set; } = 10000;
    }


    public class Response : ApiResponse<List<ResponseData>>
    {
    }

    public class ResponseData  
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? DisplayTitle { get; set; }
        public string? TitleEn { get; set; }
        public object AdditionalData { get; set; }
    }
}
