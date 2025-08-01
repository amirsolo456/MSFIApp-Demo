using MSFIApp.Dtos.Common;
using MSFIApp.Services.BaseData.Areas;
using MSFIApp.Services.Common;
using System.Collections.ObjectModel;

namespace MSFIApp.ViewModels.BaseData.Areas
{


    public class AreasViewModel : BaseViewModel
    {
        int Duration = 300;

        IAreasService<MSFIApp.Dtos.BaseData.Areas.Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>, MSFIApp.Dtos.BaseData.Areas.Request> _AreasService;

        public AreasViewModel(IAreasService<MSFIApp.Dtos.BaseData.Areas.Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>, MSFIApp.Dtos.BaseData.Areas.Request> areasService, IMessageService messageService) : base(messageService)
        {
            _AreasService = areasService;

        }

        public async Task DoSomething(Exception ex, int duration = 300, string Context = null)
        {
            await ShowMessageAsync(ex, duration, Context);
        }

        public async Task<ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>> GetAllAreas(int ID)
        {
            try
            {
                var Coursesresponse = await _AreasService.GetAreas(new MSFIApp.Dtos.BaseData.Areas.Request() { PageSize=ID});
                if (Coursesresponse != null && Coursesresponse.Entity != null)
                {
                    return new ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>(Coursesresponse.Entity);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                await DoSomething(ex, Duration);
            }

            return new ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>();
        }




    }
}
