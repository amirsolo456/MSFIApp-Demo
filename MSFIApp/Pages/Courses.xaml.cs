using MSFIApp.Dtos.Public.CoursesListGroupService;
using MSFIApp.Services.Common;
using MSFIApp.ViewModels.Public.CoursesGroup;
using MSFIApp.ViewModels.Public.EducationList;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Button = Microsoft.Maui.Controls.Button;


namespace MSFIApp.Pages;

[QueryProperty(nameof(mId), "mId")]
public partial class Courses : ContentPage, IQueryAttributable, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    private int SelectedTabID = 3;
    private int SelectedBtnID = 5;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    public int mId { get; set; }
    private readonly CoursesListGroupViewModel _CoursesGroupViewModel;
    private readonly EducationListViewModel _EducationListViewModel;
    public Courses(CoursesListGroupViewModel CoursesGroupViewModel, EducationListViewModel educationListViewModel)
    {
        try
        {
            InitializeComponent();
            _CoursesGroupViewModel = CoursesGroupViewModel;
            _EducationListViewModel = educationListViewModel;
        }
        catch (Exception ex)
        {


        }
    }

    private async Task WaitForButtonToRender(VisualElement element)
    {
        int retries = 10;
        while ((element.Width == 0 || element.Height == 0) && retries-- > 0)
        {
            await Task.Delay(50);
        }
    }

    private bool _formLoading = false;
    public bool FormLoading
    {
        get => _formLoading;
        set
        {
            _formLoading = value;
            OnPropertyChanged(nameof(FormLoading));

            if (firstBtn != null)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _ = Task.Run(async () =>
                    {
                        await Task.Delay(100);
                        await WaitForButtonToRender(firstBtn);
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await AnimateTabIndicator(Blackborder, firstBtn);
                        });
                    });
                });
            }
        }
    }

    private bool _loading = true;
    public bool Loading
    {
        get => _loading;
        set
        {
            _loading = value;
            OnPropertyChanged(nameof(Loading));
        }
    }

    Microsoft.Maui.Controls.Button firstBtn = null;
    private ObservableCollection<MSFIApp.Dtos.Public.CoursesListGroupService.ResponseData> _headButtons = new ObservableCollection<Dtos.Public.CoursesListGroupService.ResponseData>();
    public ObservableCollection<MSFIApp.Dtos.Public.CoursesListGroupService.ResponseData> HeadButtons
    {
        get => _headButtons;
        set
        {
            _headButtons = value;
            BuildButtons(HeadButtons);
        }
    }

    private ObservableCollection<MSFIApp.Dtos.Public.EducationList.ResponseData> _ontentDataShared = new ObservableCollection<Dtos.Public.EducationList.ResponseData>();
    public ObservableCollection<MSFIApp.Dtos.Public.EducationList.ResponseData> ContentDataShared
    {
        get => _ontentDataShared;
        set
        {
            _ontentDataShared = value;
            OnPropertyChanged(nameof(ContentDataShared));
        }
    }

    private ObservableCollection<MSFIApp.Dtos.Public.EducationList.ResponseData> _contentDataMens = new ObservableCollection<Dtos.Public.EducationList.ResponseData>();
    public ObservableCollection<MSFIApp.Dtos.Public.EducationList.ResponseData> ContentDataMens
    {
        get => _contentDataMens;
        set
        {

            _contentDataMens = value;
            OnPropertyChanged(nameof(ContentDataMens));

        }
    }

    private ObservableCollection<MSFIApp.Dtos.Public.EducationList.ResponseData> _contentDataWomens = new ObservableCollection<Dtos.Public.EducationList.ResponseData>();
    public ObservableCollection<MSFIApp.Dtos.Public.EducationList.ResponseData> ContentDataWomens
    {
        get => _contentDataWomens;
        set
        {

            _contentDataWomens = value;
            OnPropertyChanged(nameof(ContentDataWomens));

        }
    }



    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("mId", out object ID))
        {
            if (ID != null)
            {
                try
                {
                    Loading = true;
                    var coursesTask = _CoursesGroupViewModel?.GetCoursese(mId);
                    var educationTask = _EducationListViewModel?.GetEducations(SelectedBtnID);

                    await Task.WhenAll(coursesTask, educationTask);
                    var a = (await coursesTask);
                    var b= (await educationTask);
                    if (a.IsFailure || b.IsFailure)
                    {
                        if(a.Error != null && a.Error.Message != null)
                        {
                            MainThread.InvokeOnMainThreadAsync(async () =>
                            {
                                await ErrorPopup.ShowAsync(a.Error.Message);
                            });
                     
                        }
                        else if (b.Error != null && b.Error.Message != null)
                        {
                            MainThread.InvokeOnMainThreadAsync(async () =>
                            {
                                await ErrorPopup.ShowAsync(b.Error.Message);
                            });
                        
                        }
                        cardObj.ChnageTurn(true);
                        return;
                    }
 
                    ObservableCollection<ResponseData>Cresponse = a.Entity;
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        HeadButtons = new ObservableCollection<ResponseData>(Cresponse);
                    });

                    TransferDatas(b.Entity);
                }
                catch (Exception ex) 
                {

                }
                finally
                {
                    FormLoading = true;
                }
            }
        }
    }



    private void TransferDatas(List<MSFIApp.Dtos.Public.EducationList.ResponseData> eduresponse)
    {
        try
        {
            List<MSFIApp.Dtos.Public.EducationList.ResponseData> Eduresponse = eduresponse;
 
            var shared = new ObservableCollection<MSFIApp.Dtos.Public.EducationList.ResponseData>();
            var mens = new ObservableCollection<MSFIApp.Dtos.Public.EducationList.ResponseData>();
            var womens = new ObservableCollection<MSFIApp.Dtos.Public.EducationList.ResponseData>();

            foreach (var item in Eduresponse)
            {
                if (item.SexId == 0)
                    shared.Add(item);
                else if (item.SexId == 1)
                    womens.Add(item);
                else if (item.SexId == 2)
                    mens.Add(item);
            }

            ContentDataShared = shared;
            ContentDataMens = mens;
            ContentDataWomens = womens;
       
        }
        catch (Exception ex)
        {

 
        }
        finally
        {
            Loading = false;
        }
    }

    private async void GetBackClick(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync("///Login");
    }

    private void BuildButtons(ObservableCollection<MSFIApp.Dtos.Public.CoursesListGroupService.ResponseData> data)
    {
        if (ButtonsGrid is null || data is null) return;

        ButtonsGrid.Children.Clear();
        ButtonsGrid.ColumnDefinitions.Clear();
        ButtonsGrid.Padding = new Thickness(18, 0, 18, 0);

        for (int i = 0; i < data.Count; i++)
        {
            ButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var button = new Button();
            ButtonsGrid.Children.Add(button);
            Grid.SetColumn(button, i);

            button.Text = data[i].Title ?? string.Empty;
            button.FontSize = 14;
            button.TextColor = Colors.Gray;
            button.BackgroundColor = Colors.Transparent;
            button.FontAttributes = FontAttributes.Bold;
            button.FontFamily = "IRANSans";
            button.BindingContext = data[i].Id;
            button.HorizontalOptions = LayoutOptions.Center;
            button.VerticalOptions = LayoutOptions.Center;

            button.Clicked += (s, e) => Button_Clicked(s, e);

            if (data[i].Id == SelectedBtnID) firstBtn = button;
        }
    }


    bool Busy = false;
    private async Task GetEducationDataById(int Itemid)
    {
        try
        {
            Loading = true;

            await Task.Delay(2000); // تست موقت جایگزین GetEducations
            var educationTask = _EducationListViewModel?.GetEducations(Itemid);
            var b = (await educationTask);
            if (b.IsFailure)
            {
                await ErrorPopup.ShowAsync(b.Error.Message);
            }
            else
            {
                TransferDatas(b.Entity);
            }
           
        }
        finally
        {
            Loading = false;
        }
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (Busy) return;
        Busy = true;

        try
        {
            if (sender is Button btn && btn.BindingContext is int itemId)
            {
                SelectedBtnID = itemId;
                DisableAllButtons();
                btn.IsEnabled = false;

                foreach (Button item in ButtonsGrid)
                    item.TextColor = Colors.Gray;

                btn.TextColor = Colors.Black;
                btn.FontAttributes = FontAttributes.Bold;

                await AnimateTabIndicator(Blackborder, btn);
                await GetEducationDataById(itemId);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            Busy = false;
            EnableAllButtons();
        }
    }

    private void DisableAllButtons()
    {
        foreach (var item in ButtonsGrid)
        {
            if (item is Button btn)
                btn.IsEnabled = false;
        }
    }

    private void EnableAllButtons()
    {
        foreach (var item in ButtonsGrid)
        {
            if (item is Button btn)
                btn.IsEnabled = true;
        }
    }

    private async Task AnimateTabIndicator(View indicator, VisualElement tabButton)
    {
        if (indicator is null || tabButton is null) return;
        try
        {
            var parent = indicator.Parent as VisualElement;
            var location = tabButton.GetBoundingBoxRelativeTo(parent);

            double targetX = -location.X;
            double targetWidth = tabButton.Width;

            // انیمیشن تغییر سایز به صورت async
            var widthTask = indicator.WidthRequestTo(targetWidth, 300, Easing.CubicInOut);

            // انیمیشن جابجایی
            var moveTask = indicator.TranslateTo((targetX + 10), 0, 300, Easing.CubicInOut);

            await Task.WhenAll(widthTask, moveTask);
        }
        catch (Exception ex)
        {

            throw;
        }

    }

}

public static class ViewExtensions
{
    public static Task WidthRequestTo(this VisualElement view, double newWidth, uint length, Easing easing)
    {
        var tcs = new TaskCompletionSource<bool>();

        var animation = new Animation(v => view.WidthRequest = v, view.WidthRequest, newWidth);
        animation.Commit(view, "WidthRequestAnimation", 16, length, easing, (v, c) => tcs.SetResult(true));

        return tcs.Task;
    }
}
