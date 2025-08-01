using MSFIApp.Services.Common;
using System.ComponentModel.DataAnnotations;

namespace MSFIApp.Dtos.User.SignUp
{
    public class Request
    {
        public int ID { get; set; } = 0;
        public int AreaId { get; set; }

        public string BirthDay { get; set; } 

        public int CityId { get; set; } = 0;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public string NaturalCode { get; set; }

        public int Sex { get; set; }
    }

    public class Response : ApiResponse<ResponseData>
    {

    }

    public class ResponseData
    {

    }
}
