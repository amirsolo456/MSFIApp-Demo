using MSFIApp.Dtos.Common;
using MSFIApp.Services.BaseData.Cities;
using MSFIApp.Services.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSFIApp.ViewModels.BaseData.Cities
{
    public class CitiesViewModel : BaseViewModel
    {
        int Duration = 300;

        ICitiesService<MSFIApp.Dtos.BaseData.Cities.Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>, MSFIApp.Dtos.BaseData.Cities.Request> _CitiesService;

        public CitiesViewModel(ICitiesService<MSFIApp.Dtos.BaseData.Cities.Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>, MSFIApp.Dtos.BaseData.Cities.Request> citiesService, IMessageService messageService) : base(messageService)
        {
            _CitiesService = citiesService;

        }

        public async Task DoSomething(Exception ex, int duration = 300, string Context = null)
        {
            await ShowMessageAsync(ex, duration, Context);
        }

        public async Task<ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>> GetAllCities(int pgSize,int Areaid)
        {
            try
            {
                var Coursesresponse = await _CitiesService.GetCities(new MSFIApp.Dtos.BaseData.Cities.Request() { PageSize = pgSize, AreaId = Areaid });
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
