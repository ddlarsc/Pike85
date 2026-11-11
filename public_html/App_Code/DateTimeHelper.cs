using System;
using System.Globalization;

namespace App_Code
{
    public static class DateTimeHelper
    {
        public static string DateToStringInGuiFormat(DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy");
        }

        public static string DateToStringInDbFormat(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static DateTime StringToDateInDanishFormat(string dateTime)
        {
            var info = new CultureInfo("da-DK",true).DateTimeFormat;
            return DateTime.Parse(dateTime,info);
        }

        public static string StringToStringInGuiFormat(string dateTime)
        {
            return DateTime.Parse(dateTime).ToString("dd-MM-yyyy");
        }
    }
}