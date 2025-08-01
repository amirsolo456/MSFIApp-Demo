using MSFIApp.Services.Common;
using System.Collections.ObjectModel;

namespace MSFIApp.Dtos.BaseData.ClassRegisttration
{
    public class Request
    {
        public string Otp { get; set; }
        public int UserId { get; set; }
    }

    public class Response : ApiResponse<ObservableCollection<ResponseData>>
    {

    }

    public class ResponseData
    {
        public int? Id { get; set; }
        public string? Title { get; set; }

        /// <summary>
        /// چون محتوایی نداشت هیچکدوم!
        /// نمیدونم از چه کلاسیه
        /// </summary>
        public List<string>? ClassRegisterationPeople { get; set; }
    }
}
