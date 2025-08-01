using MSFIApp.Services.Common;

namespace MSFIApp.Dtos.User.PasswordRecovery
{
    public class Request
    {
        public string userName { get; set; }
    }

    public class Response : ApiResponse<List<ResponseData>>
    {

    }

    public class ResponseData
    {

    }
}
