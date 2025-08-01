using MSFIApp.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSFIApp.Dtos.User.SetPassword
{
    public class Request
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }

    public class Response : ApiResponse<ResponseData>
    {

    }

    public class ResponseData
    {

    }
}
