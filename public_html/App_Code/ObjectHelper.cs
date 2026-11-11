using System;
using System.Globalization;

namespace App_Code
{
    public static class ObjectHelper
    {
        public static int GetIntegerFromObject(object arg)
        {
            if(arg != null)
            {
                if(arg.GetType().Equals(typeof(int)))
                {
                    return Convert.ToInt32(arg);
                }
                if(arg.GetType().Equals(typeof(string)))
                {
                    return Convert.ToInt32(arg);
                }
                if(arg.GetType().Equals(typeof(sbyte)))
                {
                    return Convert.ToInt32(arg);
                }
                if(arg.GetType().Equals(typeof(uint)))
                {
                    return Convert.ToInt32(arg);
                }
                if(arg.GetType().Equals(typeof(Int16)))
                {
                    return Convert.ToInt32(arg);
                }
                if(arg.GetType().Equals(typeof(Int32)))
                {
                    return Convert.ToInt32(arg);
                }
                if(arg.GetType().Equals(typeof(Int64)))
                {
                    return Convert.ToInt32(arg);
                }
            }
            return 0;
        }

        public static DateTime GetDateFromObject(object arg)
        {
            var value = new DateTime();
            if(arg != null)
            {
                if(arg.GetType().Equals(typeof(DateTime)))
                {
                    var info = new DateTimeFormatInfo { ShortDatePattern = "yyyy-MM-dd" };
                    return Convert.ToDateTime(arg,info);
                }
            }
            return value;
        }

        public static bool GetBooleanFromObject(object arg)
        {
            if(arg != null)
            {
                if(arg.GetType().Equals(typeof(sbyte)))
                {
                    return Convert.ToBoolean(arg);
                }
                if (arg.GetType().Equals(typeof(Int16)))
                {
                    return Convert.ToBoolean(arg);
                }
                if(arg.GetType().Equals(typeof(Int32)))
                {
                    return Convert.ToBoolean(arg);
                }
                if(arg.GetType().Equals(typeof(Int64)))
                {
                    return Convert.ToBoolean(arg);
                }
            }
            return false;
        }

        public static string GetStringFormObject(object arg)
        {
            if(arg != null)
            {
                if(arg.GetType().Equals(typeof(string)))
                {
                    return Convert.ToString(arg);
                }
            }
            return string.Empty;
        }

        public static double GetDoubleFromObject(object arg)
        {
            if(arg != null)
            {
                if(arg.GetType().Equals(typeof(double)))
                {
                    return Convert.ToDouble(arg);
                }
            }
            return 0.0;
        }
    }
}