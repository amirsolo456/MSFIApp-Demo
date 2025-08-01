using MSFIApp.Services.User.UserMenu;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.ViewModels.User.UserMenu
{
    public class UserMenuViewModel : INotifyPropertyChanged
    {
        private MSFIApp.Dtos.User.UserMenu.Request request;
        private List<MSFIApp.Dtos.User.UserMenu.ResponseData>? response;
        private readonly IUserMenuService<MSFIApp.Dtos.User.UserMenu.Response, MSFIApp.Dtos.User.UserMenu.ResponseData, MSFIApp.Dtos.User.UserMenu.Request> _userMenuService;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public UserMenuViewModel(IUserMenuService<MSFIApp.Dtos.User.UserMenu.Response, MSFIApp.Dtos.User.UserMenu.ResponseData, MSFIApp.Dtos.User.UserMenu.Request> userMenuService)
        {
            _userMenuService = userMenuService;
        }

        public UserMenuViewModel()
        {

        }

        public async void GetData(int UserId)
        {
            try
            {
                request = new Dtos.User.UserMenu.Request()
                {
                    UserId = UserId,
                };

                var res = await _userMenuService?.GetUserMenuData(request);
                response = res.Entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
