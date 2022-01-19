using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Project.Converters
{
    // تبدیل کننده Margin برای ویدئوهای مشابه
    [ValueConversion(typeof(int), typeof(Thickness))]
    public class SimilarVideosMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemsCount = (int)value;
            if (itemsCount >= 5)
                return new Thickness(0, 0, -13, 0);
            else
                return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}