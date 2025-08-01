using MSFIApp.Dtos.BaseData.ClassRegisttration;
using MSFIApp.Dtos.Common;
using MSFIApp.Dtos.User.SignUp;
using MSFIApp.Services.BaseData.ClassRegistration;
using MSFIApp.Services.Common;
using MSFIApp.Services.User.SignUp;

namespace MSFIApp.ViewModels.User.SignUp
{
    public class SignUpViewModel : BaseViewModel
    {
        private ISignUpService<MSFIApp.Dtos.User.SignUp.Response, MSFIApp.Dtos.User.SignUp.ResponseData, MSFIApp.Dtos.User.SignUp.Request> _SignUpService;
        private Dtos.User.SignUp.Request request;
        private ApiResponse<Dtos.User.SignUp.Response> response;
        IMessageService MService;
        public SignUpViewModel(ISignUpService<MSFIApp.Dtos.User.SignUp.Response, MSFIApp.Dtos.User.SignUp.ResponseData, MSFIApp.Dtos.User.SignUp.Request> signUpService, IMessageService messageService) : base(messageService)
        {
            _SignUpService = signUpService;
            MService = messageService;
        }

        public SignUpViewModel(IMessageService messageService) : base(messageService)
        {
            MService = Microsoft.Maui.Controls.Application.Current?.Handler?.MauiContext?.Services.GetService<IMessageService>();
            _SignUpService = Microsoft.Maui.Controls.Application.Current?.Handler?.MauiContext?.Services.GetService<ISignUpService<MSFIApp.Dtos.User.SignUp.Response, MSFIApp.Dtos.User.SignUp.ResponseData, MSFIApp.Dtos.User.SignUp.Request>>();
        }



        public async Task<ApiResponse<MSFIApp.Dtos.User.SignUp.ResponseData>> GoRegister(MSFIApp.Dtos.User.SignUp.Request request)
        {

            try
            {
                var response = await _SignUpService?.SignUpGetData(request);
                return response;
            }
            catch (Exception ex)
            {

                return new Dtos.User.SignUp.Response()
                {
                    Error = new ApiError
                    {
                        Message = ex.Message,
                        Code = ex.Message
                    }
                };
            }

        }
    }
}
