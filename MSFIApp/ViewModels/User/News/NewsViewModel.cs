using MSFIApp.Dtos.User.News;
using MSFIApp.Services.Common;
using MSFIApp.Services.Main;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.ViewModels.User.News
{
    public class NewsViewModel : INotifyPropertyChanged
    {
        private ApiResponse< MSFIApp.Dtos.User.News.ResponseData> _response;
        private Request _request;
        private readonly INewsService<Response, ResponseData, Request> _mainService;

        public event PropertyChangedEventHandler PropertyChanged;

        public NewsViewModel(INewsService<Response, ResponseData, Request> mainService)
        {
            _mainService = mainService;
            LoadData();
        }



        public NewsViewModel() { }

        public ApiResponse<MSFIApp.Dtos.User.News.ResponseData> Response
        {
            get => _response;
            set
            {
                _response = value;
                OnPropertyChanged();
            }
        }

        public async void LoadData()
        {
            if (_mainService != null)
            {
                _request = new Request();
                var data = await _mainService.GetNewsData(_request);
                Response = data;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
