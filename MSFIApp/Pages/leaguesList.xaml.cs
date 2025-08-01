using MSFIApp.ViewModels.Public.CoursesGroup;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MSFIApp.Pages;

public partial class leaguesList : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private readonly CoursesListGroupViewModel _CoursesGroupViewModel;
    private int Id = 2;

    private ObservableCollection<MSFIApp.Dtos.Public.CoursesListGroupService.ResponseData> _datas = new ObservableCollection<MSFIApp.Dtos.Public.CoursesListGroupService.ResponseData>();
    public ObservableCollection<MSFIApp.Dtos.Public.CoursesListGroupService.ResponseData> Datas
    {
        get => _datas;
        set
        {
            _datas = value;
            OnPropertyChanged(nameof(Datas));
        }
    }

    public leaguesList(CoursesListGroupViewModel coursesGroupViewModel)
    {
        InitializeComponent();
        _CoursesGroupViewModel = coursesGroupViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetDatas();
    }

    private async void GetDatas()
    {
        try
        {
            var responmse = await _CoursesGroupViewModel.GetCoursese(Id);
            if (responmse != null)
            {
                if (responmse.IsFailure)
                {
                    MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await ErrorPopup.ShowAsync(responmse.Error.Message);
                    });
 
                }
                else
                {
                    var a = responmse;

                    Datas = new ObservableCollection<Dtos.Public.CoursesListGroupService.ResponseData>(a.Entity);
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private async void GetBackClick(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync("///Login");
    }
}