using MSFIApp.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSFIApp.Components.Controls.AreaPopup;

namespace MSFIApp.Dtos.BaseData.Cities
{
    public class Request
    {
        public int PageSize { get; set; } = 10000;
        public int AreaId { get; set; } = 1;
    }

    public class Response : ApiResponse<List<MSFIApp.Dtos.BaseData.Areas.ResponseData>>
    {

    }

    //public class ResponseData : ISelectableItem
    //{
    //    public int? Id { get; set; }
    //    public string? Title { get; set; }
    //    public string? DisplayTitle { get; set; }
    //    public string? TitleEn { get; set; }
    //    public object AdditionalData { get; set; }
    //}
}
