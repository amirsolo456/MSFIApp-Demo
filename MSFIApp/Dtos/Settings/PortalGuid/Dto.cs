using MSFIApp.Services.Common;

namespace MSFIApp.Dtos.Settings.PortalGuid
{
    public class Request
    {
        //public string userName { get; set; }
        //public string password { get; set; }
    }

    public class Response : ApiResponse<List<ResponseData>>
    {

    }

    public class ResponseData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
