namespace MSFIApp.Components.Controls
{
    public partial class SvgShower : ContentView
    {
        public SvgShower()
        {
            InitializeComponent();
        }

        // Source عکس یا آیکون (string می‌تواند آدرس فایل، URL یا Resource باشد)
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source), typeof(string), typeof(SvgShower), default(string), propertyChanged: OnSourceChanged);

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        private static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SvgShower control && newValue is string newSource)
            {
                control.ImageViewer.Source = newSource;
            }
        }

        // عرض تصویر
        public static readonly BindableProperty ImageWidthProperty = BindableProperty.Create(
            nameof(ImageWidth), typeof(double), typeof(SvgShower), 100.0);

        public double ImageWidth
        {
            get => (double)GetValue(ImageWidthProperty);
            set => SetValue(ImageWidthProperty, value);
        }

        // ارتفاع تصویر
        public static readonly BindableProperty ImageHeightProperty = BindableProperty.Create(
            nameof(ImageHeight), typeof(double), typeof(SvgShower), 100.0);

        public double ImageHeight
        {
            get => (double)GetValue(ImageHeightProperty);
            set => SetValue(ImageHeightProperty, value);
        }
    }
}
