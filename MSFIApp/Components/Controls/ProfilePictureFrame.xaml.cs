
namespace MSFIApp.Components.Controls;

public partial class ProfilePictureFrame : ContentView
{

    public ProfilePictureFrame()
    {
        InitializeComponent();
        UpdateImageSource();
    }

    // Property for the profile image source
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(ProfilePictureFrame), default(ImageSource), propertyChanged: OnImageSourceChanged);

    // Property for default image source (optional)
    public static readonly BindableProperty DefaultImageSourceProperty =
        BindableProperty.Create(nameof(DefaultImageSource), typeof(ImageSource), typeof(ProfilePictureFrame), default(ImageSource));

    public ImageSource ImageSource
    {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public ImageSource DefaultImageSource
    {
        get => (ImageSource)GetValue(DefaultImageSourceProperty);
        set => SetValue(DefaultImageSourceProperty, value);
    }

    private static void OnImageSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (ProfilePictureFrame)bindable;
        control.UpdateImageSource();
    }

    private void UpdateImageSource()
    {
        if (ImageSource != null)
        {
            profileImage.Source = ImageSource;
        }
        else if (DefaultImageSource != null)
        {
            profileImage.Source = DefaultImageSource;
        }
        else
        {
            profileImage.Source = "default_profile_picture.png";
        }
    }
}