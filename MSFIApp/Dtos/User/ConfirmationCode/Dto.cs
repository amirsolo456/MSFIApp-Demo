using MSFIApp.Services.Common;

namespace MSFIApp.Dtos.User.ConfirmationCode
{
    public class Request
    {
        public string Otp { get; set; }
        public int UserId { get; set; }
    }

    public class Response : ApiResponse<ResponseData>
    {

    }

    public class ResponseData
    {

    }
}
