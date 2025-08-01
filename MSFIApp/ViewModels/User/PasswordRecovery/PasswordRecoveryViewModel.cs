using MSFIApp.Dtos.User.PasswordRecovery;
using MSFIApp.Services.Common;
using MSFIApp.Services.User.PasswordRecovery;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.ViewModels.User.PasswordRecovery
{
    public class PasswordRecoveryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private readonly IPasswordRecoveryService<Response, List<ResponseData>,Request> _passwordRecoveryService;
        public PasswordRecoveryViewModel(IPasswordRecoveryService<Response, List<ResponseData>,Request> passwordRecoveryService)
        {
            _passwordRecoveryService = passwordRecoveryService;
        }

        public async Task<ApiResponse<List<ResponseData>>> SendRequest(string username)
        {
 
            try
            {
                var response = await _passwordRecoveryService.GetPasswordRecoveryData(new Request() { userName = username });
                return response;
            }
            catch (Exception ex)
            {

                return new ApiResponse<List<ResponseData>>()
                {
                    Error = new ApiError
                    {
                        Message = ex.Message,
                        Code = ex.Message,
                    }
                };
            }
 
        }

    }
}
