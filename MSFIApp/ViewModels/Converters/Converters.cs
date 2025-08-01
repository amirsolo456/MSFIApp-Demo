using MSFIApp.Dtos.Public.EducationList;
using System.Collections.ObjectModel;
using System.Globalization;

namespace MSFIApp.ViewModels.Converters
{
    public class IndexToBackgroundConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Colors.Transparent;

            int selectedIndex = System.Convert.ToInt32(value);
            int targetIndex = System.Convert.ToInt32(parameter);

            return selectedIndex == targetIndex
                ? Color.FromArgb("#030852") // رنگ تب فعال
                : Colors.Transparent;       // رنگ تب غیرفعال
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class IndexToTextColorConverter : IValueConverter
    {
        private readonly Color ActiveColor = Colors.White;
        private readonly Color InactiveColor = Color.FromArgb("#030852");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return InactiveColor;

            int selectedIndex = System.Convert.ToInt32(value);
            int tabIndex = System.Convert.ToInt32(parameter);

            return selectedIndex == tabIndex ? ActiveColor : InactiveColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }

    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return true;
            else if (value is ObservableCollection<ResponseData> dt)
            {
                if (dt.Count() == 0) return true;
                return false;
            }
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    //public class SignupDataAnnotatopn : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is null ||  value.ToString() == "")
    //        {
    //            return true;
    //        }
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //        => throw new NotImplementedException();
    //}

    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            !(value is bool boolValue) || !boolValue;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            !(value is bool boolValue) || !boolValue;
    }

}
