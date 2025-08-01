namespace MSFIApp.Components.Objects;
using Microsoft.Maui.Controls;

public partial class CourseRegisteryFooter : ContentView
{
    private const int ExpandedHeight = 100;
    private const int CollapsedHeight = -40;

    private bool _isExpanded = true;

    public double DrawerHeight { get; set; } = ExpandedHeight;
    public double ArrowRotation { get; set; } = 180;
    public CourseRegisteryFooter()
    {
        InitializeComponent();

        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += TapGestureRecognizer_Tapped;
        BottomDrawer.GestureRecognizers.Add(tapGesture);
    }


    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        //if (_isExpanded)
        //{
        //    await BottomDrawer.TranslateTo(0, 100 , 250, Easing.CubicOut);
        //    ArrowIcon.RotateTo(0, 200, Easing.CubicIn);
        //}
        //else
        //{
        //    await BottomDrawer.TranslateTo(0, -100, 250, Easing.CubicOut);
        //    ArrowIcon.RotateTo(180, 200, Easing.CubicIn);
        //}

        //_isExpanded = !_isExpanded;
    }

    private async void ArrowIcon_Clicked(object sender, EventArgs e)
    {
        if (_isExpanded)
        {
            await BottomDrawer.TranslateTo(0, ExpandedHeight, 250, Easing.CubicOut);
            //ArrowIcon.RotateTo(0, 200, Easing.CubicIn);
        }
        else
        {
            await BottomDrawer.TranslateTo(0, CollapsedHeight, 250, Easing.CubicOut);
            //ArrowIcon.RotateTo(180, 200, Easing.CubicIn);
        }

        _isExpanded = !_isExpanded;
    }
}