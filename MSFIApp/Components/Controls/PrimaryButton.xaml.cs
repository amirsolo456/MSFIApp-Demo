using System.Windows.Input;

namespace MSFIApp.Components.Controls
{
    public partial class PrimaryButton : ContentView
    {

        public PrimaryButton()
        {
            try
            {
                InitializeComponent();
                ButtonLabel.Text = MainText;
                ButtonFrame.BackgroundColor = NormalBackgroundColor;
            }
            catch (Exception ex)
            {

            }
        }

        public event EventHandler OnClick;
        // متن اصلی دکمه
        public static readonly BindableProperty MainTextProperty = BindableProperty.Create(
            nameof(MainText), typeof(string), typeof(PrimaryButton), "Button", propertyChanged: OnMainTextChanged);

        public string MainText
        {
            get => (string)GetValue(MainTextProperty);
            set => SetValue(MainTextProperty, value);
        }

        private static void OnMainTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                var control = (PrimaryButton)bindable;
                if (!control.IsBusy)
                    control.ButtonLabel.Text = (string)newValue;
            }
            catch (Exception ex)
            {

            }
        }

        // متن هنگام لودینگ
        public static readonly BindableProperty LoadingTextProperty = BindableProperty.Create(
            nameof(LoadingText), typeof(string), typeof(PrimaryButton), "Loading...");

        public string LoadingText
        {
            get => (string)GetValue(LoadingTextProperty);
            set => SetValue(LoadingTextProperty, value);
        }

        // رنگ پس زمینه عادی
        public static readonly BindableProperty NormalBackgroundColorProperty = BindableProperty.Create(
            nameof(NormalBackgroundColor), typeof(Color), typeof(PrimaryButton), Colors.Blue, propertyChanged: OnNormalBackgroundColorChanged);

        public Color NormalBackgroundColor
        {
            get => (Color)GetValue(NormalBackgroundColorProperty);
            set => SetValue(NormalBackgroundColorProperty, value);
        }

        private static void OnNormalBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                var control = (PrimaryButton)bindable;
                if (!control.IsBusy)
                    control.ButtonFrame.BackgroundColor = (Color)newValue;
            }
            catch (Exception ex)
            {


            }
        }

        // رنگ پس زمینه هنگام لودینگ
        public static readonly BindableProperty LoadingBackgroundColorProperty = BindableProperty.Create(
            nameof(LoadingBackgroundColor), typeof(Color), typeof(PrimaryButton), Colors.Gray);

        public Color LoadingBackgroundColor
        {
            get => (Color)GetValue(LoadingBackgroundColorProperty);
            set => SetValue(LoadingBackgroundColorProperty, value);
        }



        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
    nameof(IsBusy), typeof(bool), typeof(PrimaryButton), false, propertyChanged: OnIsBusyChanged);

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        private static void OnIsBusyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                var control = (PrimaryButton)bindable;
                control.UpdateLoadingState((bool)newValue);
            }
            catch (Exception ex)
            {


            }
        }

        private void UpdateLoadingState(bool isBusy)
        {
            try
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {

                    IsBusy = isBusy;
                    LoadingIndicator.IsVisible = isBusy;
                    LoadingIndicator.IsRunning = isBusy;

                    ButtonLabel.Text = isBusy ? LoadingText : MainText;
                    ButtonFrame.BackgroundColor = isBusy ? LoadingBackgroundColor : NormalBackgroundColor;
                });
            }
            catch (Exception ex)
            {

            }
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(PrimaryButton), Colors.White);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty LoadingColorProperty =
            BindableProperty.Create(nameof(LoadingColor), typeof(Color), typeof(PrimaryButton), Colors.White);

        public Color LoadingColor
        {
            get => (Color)GetValue(LoadingColorProperty);
            set => SetValue(LoadingColorProperty, value);
        }

        private async void OnTapped(object sender, EventArgs e)
        {
            if (IsBusy) return;

            try
            {
                if (Command?.CanExecute(null) == true)
                {
                    Command.Execute(null);
                }
                else
                {
                    OnClick?.Invoke(this, e);
                }
            }
            catch (Exception ex)
            {
            }
        }


        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
    nameof(IsEnabled), typeof(bool), typeof(PrimaryButton), true, propertyChanged: OnIsEnabledChanged);

        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
            nameof(IsVisible), typeof(bool), typeof(PrimaryButton), true, BindingMode.TwoWay, propertyChanged: OnIsVisibleChanged);

        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
    nameof(Command), typeof(ICommand), typeof(PrimaryButton));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }


        private static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (PrimaryButton)bindable;
            var isVisible = (bool)newValue;
            control.IsVisible = isVisible;
        }

        private static void OnIsEnabledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (PrimaryButton)bindable;
            var isEnabled = (bool)newValue;

            control.UpdateEnabledState(isEnabled);
        }



        private void UpdateEnabledState(bool isEnabled)
        {
            // برای جلوگیری از کلیک در زمان غیرفعال
            this.InputTransparent = !isEnabled;

            // کاهش Opacity برای حالت Disabled
            ButtonFrame.Opacity = isEnabled ? 1.0 : 0.5;

            // حتی اگر GestureRecognizer فعال باشه، با InputTransparent کلیک غیرفعال میشه
        }
    }
}
