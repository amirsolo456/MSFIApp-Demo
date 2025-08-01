namespace MSFIApp.ViewModels.Common;

public class PanDirectionBehavior : Behavior<View>
{
    private double _xTotal = 0;
    private double _yTotal = 0;
    private double _lastX = 0;
    private double _lastY = 0;
    private PanGestureRecognizer _panGesture;

    public event Action SwipedLeft;
    public event Action SwipedRight;

    protected override void OnAttachedTo(View bindable)
    {
        base.OnAttachedTo(bindable);
        _panGesture = new PanGestureRecognizer();
        _panGesture.PanUpdated += OnPanUpdated;
        bindable.GestureRecognizers.Add(_panGesture);
    }

    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                _xTotal = 0;
                _yTotal = 0;
                _lastX = e.TotalX;
                _lastY = e.TotalY;
                break;

            case GestureStatus.Running:
                _xTotal += Math.Abs(e.TotalX - _lastX);
                _yTotal += Math.Abs(e.TotalY - _lastY);
                _lastX = e.TotalX;
                _lastY = e.TotalY;
                break;

            case GestureStatus.Completed:
            case GestureStatus.Canceled:
                if (_xTotal > _yTotal && _xTotal > 40)
                {
                    if (e.TotalX > 0)
                        SwipedRight?.Invoke();
                    else
                        SwipedLeft?.Invoke();
                }
                _xTotal = 0;
                _yTotal = 0;
                break;
        }
    }

    protected override void OnDetachingFrom(View bindable)
    {
        base.OnDetachingFrom(bindable);
        _panGesture.PanUpdated -= OnPanUpdated;
        bindable.GestureRecognizers.Remove(_panGesture);
    }
}
