using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Project.Converters
{
    // تبدیل کننده عرض لیست ژانرها در بخش مدیریت
    [ValueConversion(typeof(double), typeof(double))]
    public class AdminGenresListWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var prentWidth = (double)value;
            return prentWidth - 5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var width = (double)value;
            return width + 5;
        }
    }
}