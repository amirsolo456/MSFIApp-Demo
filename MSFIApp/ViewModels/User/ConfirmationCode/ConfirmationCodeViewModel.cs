using MSFIApp.Dtos.User.ConfirmationCode;
using MSFIApp.Services.User.ConfirmationCode;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.ViewModels.User.ConfirmationCode
{
    public class ConfirmationCodeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private readonly IConfirmationCodeService<Response, ResponseData,Request> _ConfirmationCodeService;
        public ConfirmationCodeViewModel(IConfirmationCodeService<Response, ResponseData, Request> confirmationCodeService)
        {
            try
            {
                _ConfirmationCodeService = confirmationCodeService;
            }
            catch (Exception ex)
            {


            }   
        }

        public async Task<bool> CheckCode(string Code, int userid)
        {
            bool b = false;
            try
            {
                var response = await _ConfirmationCodeService.CheckConfirmationCode(new Request() { Otp = Code, UserId = userid });
                if (response != null)
                {
                    if (response.Id != 0 && response.Status == 1)
                    {
                        b = true;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return b;
        }

    }
}
