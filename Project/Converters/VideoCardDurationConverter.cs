using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Project.Converters
{
    // تبدیل کننده مدت زمان هر ویدئو
    [ValueConversion(typeof(int), typeof(string))]
    public class VideoCardDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int duration = (int)value;
            int hour, minute;

            hour = duration / 60;
            minute = duration % 60;

            return $"{hour} ساعت و {minute} دقیقه";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}