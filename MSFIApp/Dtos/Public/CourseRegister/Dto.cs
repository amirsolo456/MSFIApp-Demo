using MSFIApp.Services.Common;

namespace MSFIApp.Dtos.Public.CourseRegister
{
    public class Request
    {
        public int id { get; set; } = 1;
    }

    public class Response : ApiResponse<ResponseData>
    {

    }

    public class ResponseData
    {
        public bool? IsComfirmAge { get; set; }
        public bool? IsComfirmIntershipClass { get; set; }
        public bool? IsComfirmPriClass { get; set; }
        public bool? IsComfirmPriIntership { get; set; }
        public bool IsNeedComfirmPriIntership { get; set; }
        public bool? IsComfirmIntershipClassCount { get; set; }
        public bool IsNeedComfirmIntershipClassCount { get; set; }
        public bool IsComfirmRegisterDate { get; set; }
        public int PersonCeridit { get; set; }
        public bool IsHourseCategories { get; set; }
        public bool IsNeedPridoc { get; set; }
        public string? Pridoc { get; set; }
        public string? PridocDate { get; set; }
        public int? PridocDays { get; set; }
        public string? PridocName { get; set; }
        public int Id { get; set; }
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
        public string StartDate { get; set; }
        public string? PStartDate { get; set; }
        public int Duration { get; set; }
        public string RegisterStart { get; set; }
        public string? PRegisterStart { get; set; }
        public string RegisterEnd { get; set; }
        public string? PRegisterEnd { get; set; }
        public string? Tel { get; set; }
        public string? PersonName { get; set; }
        public string? Description { get; set; }
        public int VenueId { get; set; }
        public string? VenueName { get; set; }
        public int? MatchAgeLevelId { get; set; }
        public string? MatchAgeLevelName { get; set; }
        public int SexId { get; set; }
        public string? SexName { get; set; }
        public int? MatchFeldId { get; set; }
        public string? MatchFeldName { get; set; }
        public int? MatchLevelId { get; set; }
        public string? MatchLevelName { get; set; }
        public decimal Price { get; set; }
        public int UserRegisterationTypeId { get; set; }
        public int? AlterUserRegisterationTypeId { get; set; }
        public int? Alter2UserRegisterationTypeId { get; set; }
        public int Capaciry { get; set; }
        public int? ClassWorkId { get; set; }
        public int? MaxHourseCount { get; set; }
        public int Area_ID { get; set; }
        public int? ClassTitleId { get; set; }
        public int TypeCapacity { get; set; }
        public int RegisterType { get; set; }
        public int FederationPersent { get; set; }
        public bool? IsRandimize { get; set; }

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
    }

}
