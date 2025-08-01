using System.Collections.ObjectModel;
using System.ComponentModel;
using static MSFIApp.Components.Controls.CustomRadioButton;

namespace MSFIApp.Components.Controls;

public partial class CustomRadioButton : ContentView, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    public static readonly BindableProperty ValueProperty =
      BindableProperty.Create(nameof(Value), typeof(ObservableCollection<CustomRadioItems>), 
          typeof(CustomRadioButton), new ObservableCollection<CustomRadioItems>(), propertyChanged: OnValueChanged);

    public static readonly BindableProperty SelectedValueProperty =
        BindableProperty.Create(nameof(SelectedValue), typeof(int), typeof(CustomRadioButton),
            0, BindingMode.TwoWay, propertyChanged: OnSelectedValueChanged);

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(CustomRadioButton), string.Empty);

    public static readonly BindableProperty IsCheckedProperty =
    BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CustomRadioButton), false);

    public record CustomRadioItems(int TrueValue, string name);

    public ObservableCollection<CustomRadioItems> Value
    {
        get => (ObservableCollection<CustomRadioItems>)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public int SelectedValue
    {
        get => (int)GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public CustomRadioButton()
    {
        InitializeComponent();
    }

    private  void OnCheckChange(object sender, EventArgs e, CustomRadioItems CurrentRow)
    {
        if(sender is RadioButton rd)
        {
            if(rd.IsChecked)
                SelectedValue = CurrentRow.TrueValue;
        }
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var radio = bindable as CustomRadioButton;
        try
        {
            if (newValue != null && newValue is ObservableCollection<CustomRadioItems> Value)
            {
                radio.RadioGroups.Clear();
                foreach (CustomRadioItems item in Value)
                {
                    var rb = new RadioButton
                    {
                        Style = (Style)radio.Resources["MyRadio"],
                        BindingContext = item                      
                    };

                    rb.CheckedChanged += (s, e) => radio.OnCheckChange(s, e, item);

                    rb.SetBinding(RadioButton.IsCheckedProperty, nameof(radio.IsChecked));

                    var label = new Label
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Text = item.name
                    };

                    var layout = new HorizontalStackLayout
                    {
                        Spacing = 5,
                        Children = { rb, label }
                    };

                    radio.RadioGroups.Children.Add(layout);
                }

                radio.RadioGroups.Spacing = 10;
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
    }

    private static void OnSelectedValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var radio = bindable as CustomRadioButton;
        if (Convert.ToInt32(newValue) == 1)
        {
            radio.IsChecked = false;
        }
        else radio.IsChecked = true;
        radio.OnPropertyChanged(nameof(IsChecked));
    }


}