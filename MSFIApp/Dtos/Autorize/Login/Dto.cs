using MSFIApp.Services.Common;

namespace MSFIApp.Dtos.Autorize.Login
{
    public class Request
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
    public class Response : ApiResponse<ResponseData>
    {

    }

    public class ResponseData
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Token { get; set; }
        public string Type { get; set; }
        public int? PersonId { get; set; }
        public int? CotchId { get; set; } = null;
        public int? Credit { get; set; }
        public string AcountName { get; set; }
        public string? Expire { get; set; }
    }
}
