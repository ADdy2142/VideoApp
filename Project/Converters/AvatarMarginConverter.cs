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
    // تبدیل کننده Margin برای Avatar ها
    [ValueConversion(typeof(string), typeof(Thickness))]
    public class AvatarMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = value.ToString();
            var result = new Thickness(0);
            switch (name)
            {
                case "avatar-1":
                case "avatar-2":
                case "avatar-3":
                case "avatar-4":
                    result = new Thickness(0, 0, 0, 10);
                    break;

                case "avatar-5":
                case "avatar-6":
                case "avatar-7":
                case "avatar-8":
                    result = new Thickness(0, 10, 0, 0);
                    break;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}