using MSFIApp.Services.Common;

namespace MSFIApp.Dtos.Public.EducationList
{
    public class Request
    {
        public int Id { get; set; }
    }

    public class Response : ApiResponse<List<ResponseData>>
    {

    }

    public class ResponseData
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string StartDate { get; set; }
        public string StartDateOrg { get; set; }
        public int Duration { get; set; }
        public string SexName { get; set; }

        public string SexDisplay
        {
            get
            {
                return SexName switch
                {
                    "مرد" => "آقایان",
                    "زن" => "بانوان",
                    _ => SexName
                };
            }
        }
        public string RegisterExpire { get; set; }
        public string RegisterExpireOrg { get; set; }
        public string VenueName { get; set; }
        public string MatchAgeLevelName { get; set; }
        public string MatchFeldName { get; set; }
        public decimal Price { get; set; }
        public int SexId { get; set; }
        public int RemainCapacity { get; set; }
        public int Capacity { get; set; }
    }
}
