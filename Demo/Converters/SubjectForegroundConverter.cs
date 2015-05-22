using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Demo.Converters
{
    public class SubjectForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (SolidColorBrush)App.Current.Resources[(bool)value ? "PhoneAccentBrush" : "PhoneMidBrush"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}