using MSFIApp.Services.BaseData.Areas;
using MSFIApp.ViewModels.Public.CourseRegister;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MSFIApp.Pages;
[QueryProperty(nameof(courseId), "CourseId")]
public partial class CourseRegistery : ContentPage, IQueryAttributable, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string name)
    {
        try
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        catch (Exception ex)
        {

        }
    }

    public int courseId { get; set; }
    private IAreasService<MSFIApp.Dtos.BaseData.Areas.Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>, MSFIApp.Dtos.BaseData.Areas.Request> _AreasService;
    private CourseRegisterViewModel _CourseRegisterviewModel;
    string htmlFromServer = "";
    public ICommand RegisterCommand { get; }
    public ICommand MemberListCommand { get; }

    public CourseRegistery(CourseRegisterViewModel courseRegisterViewModel, IAreasService<MSFIApp.Dtos.BaseData.Areas.Response, List<MSFIApp.Dtos.BaseData.Areas.ResponseData>, MSFIApp.Dtos.BaseData.Areas.Request> areasService)
    {
        InitializeComponent();
        _CourseRegisterviewModel = courseRegisterViewModel;
        _AreasService = areasService;
        RegisterCommand = new Command<MSFIApp.Dtos.Public.EducationList.ResponseData>(OnRegister);
        MemberListCommand = new Command<MSFIApp.Dtos.Public.EducationList.ResponseData>(OnMemberList);




        //HtmlWebView.Source = new HtmlWebViewSource
        //{
        //    Html = $"<html><head><meta charset='UTF-8'></head><body dir='rtl'>{htmlFromServer}</body></html>"
        //};
    }
    private async void OnRegister(MSFIApp.Dtos.Public.EducationList.ResponseData data)
    {

    }

    private void OnMemberList(MSFIApp.Dtos.Public.EducationList.ResponseData data)
    {

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        new Thread(() => GetAreas());
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {

            if (query.TryGetValue("CourseId", out var cId))
            {

                if (cId != null)
                {

                    if (cId is string str && int.TryParse(str, out var iid))
                    {
                        courseId = cId switch
                        {
                            int i => i,
                            string s when int.TryParse(s, out var parsed) => parsed,
                            _ => throw new InvalidCastException($"CourseId type '{cId.GetType()}' is not convertible to int")
                        };

                        var response = await _CourseRegisterviewModel.GetCoursese(courseId);
                        CurrentCourse = response.Entity;
                        EduList = new Dtos.Public.EducationList.ResponseData()
                        {
                            Capacity = CurrentCourse.Capaciry,
                            Id = CurrentCourse.Id,
                            Duration = CurrentCourse.Duration,
                            ClassName = CurrentCourse.ClassName,
                            Price = CurrentCourse.Price,
                            StartDate = CurrentCourse.StartDate,
                            SexId = CurrentCourse.SexId,
                            SexName = CurrentCourse.SexName,
                            MatchFeldName = CurrentCourse.MatchFeldName,
                            VenueName = CurrentCourse.VenueName,
                            RegisterExpire = CurrentCourse.RegisterEnd
                        };
                    }
                }
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData> _areas = new ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData>();
    public ObservableCollection<MSFIApp.Dtos.BaseData.Areas.ResponseData> Areas
    {
        get => _areas;
        set
        {

            _areas = value;
            OnPropertyChanged(nameof(Areas));

        }
    }


    private MSFIApp.Dtos.Public.CourseRegister.ResponseData _currentCourses = new MSFIApp.Dtos.Public.CourseRegister.ResponseData();
    public MSFIApp.Dtos.Public.CourseRegister.ResponseData CurrentCourse
    {
        get => _currentCourses;
        set
        {

            _currentCourses = value;
            OnPropertyChanged(nameof(CurrentCourse));

        }
    }

    private MSFIApp.Dtos.Public.EducationList.ResponseData _eduList = new Dtos.Public.EducationList.ResponseData();
    public MSFIApp.Dtos.Public.EducationList.ResponseData EduList
    {
        get => _eduList;
        set
        {
            _eduList = value;
            OnPropertyChanged(nameof(EduList));
        }
    }

    private async Task GetAreas()
    {
        var AreasService = await _AreasService.GetAreas(new MSFIApp.Dtos.BaseData.Areas.Request());
        Areas = new ObservableCollection<Dtos.BaseData.Areas.ResponseData>(AreasService.Entity);
    }

    private async void IconButton_Clicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync($"///Courses?mId={1}");
    }
}