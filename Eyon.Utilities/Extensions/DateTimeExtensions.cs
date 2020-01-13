using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Eyon.Utilities.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToFriendlyString(this DateTime dateTime, string format = "MMMM dd, yyyy", CultureInfo culture = null )
        {
            DateTimeFormatInfo dtFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
            if (culture != null )
                dtFormatInfo = culture.DateTimeFormat;

            return dateTime.ToString(format, dtFormatInfo);
        }
    }
}
