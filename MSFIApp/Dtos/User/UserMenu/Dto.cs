using MSFIApp.Services.Common;

namespace MSFIApp.Dtos.User.UserMenu
{
    public class Request
    {
        public int UserId { get; set; }
    }

    public class Response : ApiResponse<List<ResponseData>>
    {

    }

    public class ResponseData
    {
        public int? Id { get; set; }
        public int? ParentID { get; set; }
        public string? Title { get; set; }
        public string? Icon { get; set; }
        public string? Navigation { get; set; }
        public string? AppAction { get; set; }
        public List<ResponseData>? Childrens { get; set; }
    }
}
