using Project.Data.Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure
{
    // این کلاس شامل متدهایی است که روی عناصر موجود در زبان سی شارپ پیاده سازی می شود و منجر به تغییرات دلخواه خواهد شد
    public static class ExtensionMethods
    {
        public static string ToLongPresianDate(this DateTime dateTime)
        {
            if (dateTime.ToShortDateString() == DateTime.Now.ToShortDateString())
                return "امروز";

            var persianCalendar = new PersianCalendar();
            string dayOfWeek = string.Empty, month = string.Empty;
            switch (persianCalendar.GetDayOfWeek(dateTime))
            {
                case DayOfWeek.Saturday:
                    dayOfWeek = "شنبه";
                    break;

                case DayOfWeek.Sunday:
                    dayOfWeek = "یکشنبه";
                    break;

                case DayOfWeek.Monday:
                    dayOfWeek = "دوشنبه";
                    break;

                case DayOfWeek.Tuesday:
                    dayOfWeek = "سه شنبه";
                    break;

                case DayOfWeek.Wednesday:
                    dayOfWeek = "چهارشنبه";
                    break;

                case DayOfWeek.Thursday:
                    dayOfWeek = "پنجشنبه";
                    break;

                case DayOfWeek.Friday:
                    dayOfWeek = "جمعه";
                    break;
            }

            switch (persianCalendar.GetMonth(dateTime))
            {
                case 1:
                    month = "فروردین";
                    break;

                case 2:
                    month = "اردیبهشت";
                    break;

                case 3:
                    month = "خرداد";
                    break;

                case 4:
                    month = "تیر";
                    break;

                case 5:
                    month = "مرداد";
                    break;

                case 6:
                    month = "شهریور";
                    break;

                case 7:
                    month = "مهر";
                    break;

                case 8:
                    month = "آبان";
                    break;

                case 9:
                    month = "آدر";
                    break;

                case 10:
                    month = "دی";
                    break;

                case 11:
                    month = "بهمن";
                    break;

                case 12:
                    month = "اسفند";
                    break;
            }

            return $"{dayOfWeek} {persianCalendar.GetDayOfMonth(dateTime)} {month} {persianCalendar.GetYear(dateTime)}";
        }

        public static void ReloadEntity<TEntity>(this ApplicationDbContext context, TEntity entity) where TEntity : class
        {
            context.Entry(entity).Reload();
        }
    }
}