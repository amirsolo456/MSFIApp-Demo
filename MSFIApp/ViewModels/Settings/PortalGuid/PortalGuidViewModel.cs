using MSFIApp.Dtos.Settings.PortalGuid;
using MSFIApp.Services.Common;
using MSFIApp.Services.PortalGuid;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.ViewModels.Settings.PortalGuid
{
    public class PortalGuidViewModel : INotifyPropertyChanged, ILoadingTryAgainService.Partision.ViewmodelProps
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ResponseData> _response = new();
        public ObservableCollection<ResponseData> Response
        {
            get => _response;
            set
            {
                _response = value;
                OnPropertyChanged();
            }
        }

        private bool _isBusy = true;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        private bool _turnVisible = false;
        public bool TurnVisible
        {
            get => _turnVisible;
            set
            {
                _turnVisible = value;
                OnPropertyChanged(nameof(TurnVisible));
            }
        }


        private Request Request;

        private IPortalGuidService<MSFIApp.Dtos.Settings.PortalGuid.Response, List<ResponseData>, MSFIApp.Dtos.Settings.PortalGuid.Request> _PortalGuidService;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public PortalGuidViewModel(IPortalGuidService<MSFIApp.Dtos.Settings.PortalGuid.Response, List<ResponseData>, MSFIApp.Dtos.Settings.PortalGuid.Request> portalGuidService)
        {
            _PortalGuidService = portalGuidService;
        }

        public PortalGuidViewModel()
        {
            _PortalGuidService = Microsoft.Maui.Controls.Application.Current?.Handler?.MauiContext?.Services.GetService<IPortalGuidService<MSFIApp.Dtos.Settings.PortalGuid.Response, List<MSFIApp.Dtos.Settings.PortalGuid.ResponseData>, MSFIApp.Dtos.Settings.PortalGuid.Request>>();
        }

        public async Task<ApiResponse<List<MSFIApp.Dtos.Settings.PortalGuid.ResponseData>>> GetPortablDataAsync()
        {
            IsBusy = true;

            Request = new Request();
            var a = await _PortalGuidService?.GetPortablData(Request);
            return a;
        }

        public async void BuildUI(List<MSFIApp.Dtos.Settings.PortalGuid.ResponseData> response)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Response = new ObservableCollection<ResponseData>(response);
                IsBusy = false;
            });
        }

        public void ChnageTurn(bool TurnVisibleValue)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TurnVisible = TurnVisibleValue;
            });
        }
    }
}
