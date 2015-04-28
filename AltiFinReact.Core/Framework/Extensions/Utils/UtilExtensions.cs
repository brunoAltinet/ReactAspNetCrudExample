using System;

namespace Altinet.Extensions.Utils
{
    public static class UtilExtensions
    {
        public static DateTime ToMonthFloor(this DateTime? date)
        {
           return date.GetValueOrDefault().ToMonthFloor();
        }

        public static DateTime ToMonthFloor(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
    }
}
