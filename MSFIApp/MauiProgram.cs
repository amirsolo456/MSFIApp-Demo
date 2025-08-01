using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MSFIApp.Dtos.Autorize.Login;
using MSFIApp.Dtos.Common;
using MSFIApp.Pages;
using MSFIApp.Services.Autorize;
using MSFIApp.Services.BaseData.Areas;
using MSFIApp.Services.BaseData.Cities;
using MSFIApp.Services.BaseData.ClassRegistration;
using MSFIApp.Services.Common;
using MSFIApp.Services.Main;
using MSFIApp.Services.PortalGuid;
using MSFIApp.Services.Public.CourseRegister;
using MSFIApp.Services.Public.CoursesGroup;
using MSFIApp.Services.Public.EducationList;
using MSFIApp.Services.Public.Favorites;
using MSFIApp.Services.User.ConfirmationCode;
using MSFIApp.Services.User.PasswordRecovery;
using MSFIApp.Services.User.SetPassword;
using MSFIApp.Services.User.SignUp;
using MSFIApp.Services.User.UserMenu;
using MSFIApp.ViewModels.BaseData.Areas;
using MSFIApp.ViewModels.BaseData.Cities;
using MSFIApp.ViewModels.Common;
using MSFIApp.ViewModels.Public.CourseRegister;
using MSFIApp.ViewModels.Public.CoursesGroup;
using MSFIApp.ViewModels.Public.EducationList;
using MSFIApp.ViewModels.Public.Favorites;
using MSFIApp.ViewModels.Settings.PortalGuid;
using MSFIApp.ViewModels.User.ConfirmationCode;
using MSFIApp.ViewModels.User.News;
using MSFIApp.ViewModels.User.PasswordRecovery;
using MSFIApp.ViewModels.User.SignUp;
using System.Collections.ObjectModel;



#if IOS || MACCATALYST
using UIKit;
#endif
namespace MSFIApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fa");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fa");
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("IRANSansX-Bold.ttf", "IRANSansX");
                fonts.AddFont("IRANSansX-Regular.ttf", "IRANSans");
                fonts.AddFont("Yekan.ttf", "Yekan");
            })
            .RegisterServices()
            .RegisterViewModels()
            .RegisterViews()
            .RegisterFeatures()
            .ConfigureEssentials(essentials =>
            {
                essentials
                    .AddAppAction("app_info", "App Info", icon: "logo.png")
                    .AddAppAction("battery_info", "Battery Info", icon: "logo.png")
                    .OnAppAction(AppActionsHandler.HandleAppActions);
            });

        builder.Services.Configure<ApiSettings>(settings =>
        {
            settings.BaseUrl = "https://base.msfi.ir";
            settings.Token_Key = "user_token";
            settings.maxRetryAttempts = 3;
            settings.delayMilliseconds = 500;
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        ThemeScrollBar();

        return builder.Build();
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IMessageService, MessageService>();
        mauiAppBuilder.Services.AddSingleton<ILoginService<Response, ResponseData, Request>, LoginService>();
        mauiAppBuilder.Services.AddSingleton<INewsService<MSFIApp.Dtos.User.News.Response, MSFIApp.Dtos.User.News.ResponseData, MSFIApp.Dtos.User.News.Request>, NewsService>();
        mauiAppBuilder.Services.AddSingleton<IUserMenuService<MSFIApp.Dtos.User.UserMenu.Response, List<MSFIApp.Dtos.User.UserMenu.ResponseData>, MSFIApp.Dtos.User.UserMenu.Request>, UserMenuService>();
        mauiAppBuilder.Services.AddSingleton<IFavoritesService<MSFIApp.Dtos.Public.Favorites.Response, ObservableCollection<MSFIApp.Dtos.Public.Favorites.ResponseData>, MSFIApp.Dtos.Public.Favorites.Request>, FavoritesService>();
        mauiAppBuilder.Services.AddSingleton<IPortalGuidService<MSFIApp.Dtos.Settings.PortalGuid.Response, List<MSFIApp.Dtos.Settings.PortalGuid.ResponseData>,MSFIApp.Dtos.Settings.PortalGuid.Request>, PortalGuidService>();//1
        mauiAppBuilder.Services.AddSingleton<IPasswordRecoveryService<MSFIApp.Dtos.User.PasswordRecovery.Response,List<MSFIApp.Dtos.User.PasswordRecovery.ResponseData>, MSFIApp.Dtos.User.PasswordRecovery.Request>, PasswordRecoveryService>();//2
        mauiAppBuilder.Services.AddSingleton<ISetPasswordService<MSFIApp.Dtos.User.SetPassword.Response, MSFIApp.Dtos.User.SetPassword.ResponseData, MSFIApp.Dtos.User.SetPassword.Request>, SetPasswordService>();
        mauiAppBuilder.Services.AddSingleton<IConfirmationCodeService<MSFIApp.Dtos.User.ConfirmationCode.Response, MSFIApp.Dtos.User.ConfirmationCode.ResponseData, MSFIApp.Dtos.User.ConfirmationCode.Request>, ConfirmationCodeService>();
        mauiAppBuilder.Services.AddSingleton<ICoursesListGroupService<MSFIApp.Dtos.Public.CoursesListGroupService.Response, ObservableCollection<MSFIApp.Dtos.Public.CoursesListGroupService.ResponseData>, MSFIApp.Dtos.Public.CoursesListGroupService.Request>, CoursesListGroupService>();//3
        mauiAppBuilder.Services.AddSingleton<IEducationListService<MSFIApp.Dtos.Public.EducationList.Response, List<MSFIApp.Dtos.Public.EducationList.ResponseData>, MSFIApp.Dtos.Public.EducationList.Request>, EducationListService>();//4
        mauiAppBuilder.Services.AddSingleton<ICourseRegisterService<MSFIApp.Dtos.Public.CourseRegister.Response, MSFIApp.Dtos.Public.CourseRegister.ResponseData, MSFIApp.Dtos.Public.CourseRegister.Request>, CourseRegisterService>();
        mauiAppBuilder.Services.AddSingleton<IAreasService<MSFIApp.Dtos.BaseData.Areas.Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>,MSFIApp.Dtos.BaseData.Areas.Request>, AreasService>();
        mauiAppBuilder.Services.AddSingleton<ICitiesService<MSFIApp.Dtos.BaseData.Cities.Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>, MSFIApp.Dtos.BaseData.Cities.Request>, CitiesService>();
        mauiAppBuilder.Services.AddSingleton<IClassRegistrationService<MSFIApp.Dtos.BaseData.ClassRegisttration.Response, ObservableCollection<MSFIApp.Dtos.BaseData.ClassRegisttration.ResponseData>, MSFIApp.Dtos.BaseData.ClassRegisttration.Request>, ClassRegistrationService>();
        mauiAppBuilder.Services.AddSingleton<ISignUpService<MSFIApp.Dtos.User.SignUp.Response, MSFIApp.Dtos.User.SignUp.ResponseData, MSFIApp.Dtos.User.SignUp.Request>, SignUpService>();
        mauiAppBuilder.Services.AddSingleton<SignUp>();
        // More services registered here.

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<NewsViewModel>();
        mauiAppBuilder.Services.AddTransient<ExpansionPannelViewModel>();
        mauiAppBuilder.Services.AddTransient<FavoritesViewModel>();
        mauiAppBuilder.Services.AddTransient<PortalGuidViewModel>();
        mauiAppBuilder.Services.AddTransient<PasswordRecoveryViewModel>();
        mauiAppBuilder.Services.AddTransient<ConfirmationCodeViewModel>();
        mauiAppBuilder.Services.AddTransient<CoursesListGroupViewModel>();
        mauiAppBuilder.Services.AddTransient<EducationListViewModel>();
        mauiAppBuilder.Services.AddTransient<CourseRegisterViewModel>();
        mauiAppBuilder.Services.AddTransient<AreasViewModel>();
        mauiAppBuilder.Services.AddTransient<SignUpViewModel>();
        mauiAppBuilder.Services.AddTransient<CitiesViewModel>();

        // More view-models registered here.

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<StartLoading>();
        mauiAppBuilder.Services.AddSingleton<PasswordMaking>();
        mauiAppBuilder.Services.AddSingleton<PortalGuid>();

        // More views registered here.

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterFeatures(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ISecureStorage>(SecureStorage.Default);
        mauiAppBuilder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();
        mauiAppBuilder.Services.AddSingleton<IApiCleint, ApiClient>();

        // More Features registered here.

        return mauiAppBuilder;
    }
    private static void ThemeScrollBar()
    {
        Microsoft.Maui.Handlers.ScrollViewHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
        {

#if IOS
			handler.PlatformView.Scrolled += (s, e) =>
			{
				var scrollView = (UIScrollView)s;
				var verticalIndicator = scrollView.Subviews.Last();
    			verticalIndicator.BackgroundColor = UIColor.Red;
				
			};
#elif MACCATALYST
            handler.PlatformView.IndicatorStyle = UIScrollViewIndicatorStyle.White;
#elif ANDROID
            //handler.PlatformView.ScrollBarStyle = Android.Views.ScrollBarStyle.InsideOverlay;
#elif WINDOWS
			// Windows customization
#endif
        });
    }
}
