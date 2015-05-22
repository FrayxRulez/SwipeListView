using System;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace Demo.Converters
{
    public class LeftTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? "Mark as Read" : "Mark as Unread";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}