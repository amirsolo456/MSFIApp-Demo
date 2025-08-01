using MSFIApp.Dtos.Public.EducationList;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
namespace MSFIApp.Components.Controls;

public partial class CardView : ContentView, INotifyPropertyChanged
{
    public enum ShowType { IsView, IsDetail };

    public new event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    public CardView()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty DataProperty =
    BindableProperty.Create(
        nameof(Data),
        typeof(ResponseData),
        typeof(CardView),
        new ResponseData(),
        propertyChanged: OnDataChanged
    );

    public static readonly BindableProperty showTypeProperty =
    BindableProperty.Create(
        nameof(showType),
        typeof(ShowType),
        typeof(CardView),
        ShowType.IsDetail,
                propertyChanged: OnShowTypeChanged
    );

    public ShowType showType
    {
        get => (ShowType)GetValue(showTypeProperty);
        set => SetValue(showTypeProperty, value);
    }


    public ResponseData Data
    {
        get => (ResponseData)GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public static readonly BindableProperty RegisterCommandProperty = BindableProperty.Create(
        nameof(RegisterCommand), typeof(ICommand), typeof(CardView));

    public ICommand RegisterCommand
    {
        get => (ICommand)GetValue(RegisterCommandProperty);
        set => SetValue(RegisterCommandProperty, value);
    }

    public static readonly BindableProperty MemberListCommandProperty = BindableProperty.Create(
        nameof(MemberListCommand), typeof(ICommand), typeof(CardView));

    public ICommand MemberListCommand
    {
        get => (ICommand)GetValue(MemberListCommandProperty);
        set => SetValue(MemberListCommandProperty, value);
    }

    private static void OnDataChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CardView)bindable;
        control.OnPropertyChanged(nameof(Data));
        control.UpdateUI();
    }

    private static void OnShowTypeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CardView)bindable;
        if (newValue is ShowType sType)
        {
            control.ViewOp.IsVisible = (sType == ShowType.IsView ? false : true);
            control.OnPropertyChanged(nameof(ViewOp));
        }
    }
 
    private void UpdateUI()
    {

        if (Data != null)
        {
            TitleLabel.Text = Data.ClassName;
            SexLabel.Text = Data.SexDisplay;
            CapacityLabel.Text = Data.RemainCapacity.ToString();
            MatchFeldLabel.Text = Data.MatchFeldName;
            AgeLevelLabel.Text = Data.MatchAgeLevelName;
            StartDateLabel.Text = Data.StartDate;
            DurationLabel.Text = Data.Duration.ToString();
            RegisterExpireLabel.Text = Data.RegisterExpire;
            VenueLabel.Text = Data.VenueName;
            PriceLabel.Text = Data.Price.ToString("N0", new CultureInfo("fr-IR"));
        }
    }

    private void OnRegisterClicked(object sender, EventArgs e)
    {
        if (RegisterCommand?.CanExecute(Data) ?? false)
            RegisterCommand.Execute(Data);
    }

    private void OnMemberListClicked(object sender, EventArgs e)
    {
        if (MemberListCommand?.CanExecute(Data) ?? false)
            MemberListCommand.Execute(Data);
    }
}