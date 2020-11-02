using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace KavoshFrameWorkCommon.Helpers
{
    public static class GeneralHelpers
    {
        public static string Truncate(this string str, int count)
        {
            try
            {
                if (str.Length < count)
                    return str;
                else
                {
                    str = str.Substring(0, count);
                    var spaceIndex = str.LastIndexOf(" ");
                    if (spaceIndex > -1)
                    {
                        return str.Substring(0, spaceIndex) + " ...";
                    }
                    else
                    {
                        return str;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return "Error";
            }
        }
        public static string GetQueryString(this object obj)
        {
            try
            {
                var properties = from p in obj.GetType().GetProperties()
                                 where p.GetValue(obj, null) != null
                                 select p.Name + "=" + p.GetValue(obj, null).ToString();

                return string.Join("&", properties.ToArray());
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return "Error";
            }
        }
        public static string GetFileFormatRegx(string type)
        {
            try
            {
                switch (type)
                {
                    case "Image":
                        return "/(jpg)|(jpeg)|(png)|(gif)$/i";
                    case "Video":
                        return "/(mp4)$/i";
                    case "Audio":
                        return "/(mp3)/i";
                    case "Attachment":
                        return "/(pdf)|(doc?)|(xls?)|(jpg)|(jpeg)|(png)|(gif)$/i";
                    default:
                        return "/(jpg)|(jpeg)|(png)|(gif)$/i";
                }

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return "Error";
            }
        }
        public static string DisplayName<T>(object option)
        {
            try
            {
                var enumType = typeof(T);
                var field = enumType.GetFields()
                           .FirstOrDefault(x => x.Name == Enum.GetName(enumType, option));

                if (field != null)
                {
                    var attributes = field.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (attributes.Any())
                    {
                        var name = ((DisplayAttribute)attributes.First()).Name;
                        return name;
                    }
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return "Error";
            }
        }
        public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                    return null;
                return httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return "Error";
            }
        }
        public static string GuidToBase64(Guid guid)
        {
            try
            {
                return Convert.ToBase64String(guid.ToByteArray()).Replace("/", "-").Replace("+", "_").Replace("=", "");

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return "Error";
            }
        }
        public static string GuidToBase64(string guid)
        {
            try
            {
                return Convert.ToBase64String(Guid.Parse(guid).ToByteArray()).Replace("/", "-").Replace("+", "_").Replace("=", "");
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return "Error";
            }
        }
        public static Guid Base64ToGuid(string base64)
        {
            try
            {
                Guid guid = default(Guid);
                base64 = base64.Replace("-", "/").Replace("_", "+") + "==";
               
                    guid = new Guid(Convert.FromBase64String(base64));
                
                return guid;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return Guid.Empty;
            }
        }

        public static string ToShamsi(this DateTime datetime, bool reverse = false)
        {
            try
            {
                var pc = new PersianCalendar();
                var convertedDateTime = Convert.ToDateTime(datetime);
                var year = pc.GetYear(convertedDateTime).ToString();
                var month = pc.GetMonth(convertedDateTime).ToString().PadLeft(2, '0');
                var day = pc.GetDayOfMonth(convertedDateTime).ToString().PadLeft(2, '0');
                if (reverse)
                    return $"{day}/{month}/{year}";
                else
                    return $"{year}/{month}/{day}";
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return "Error";
            }
        }

        public static string ToShamsi(this DateTime? datetime, bool reverse = false)
        {
            try
            {
                if (!datetime.HasValue)
                    return string.Empty;
                return ToShamsi(datetime.Value, reverse);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return "Error";
            }
        }

        public static int ToShamsiYear(this DateTime datetime)
        {
            try
            {
                var pc = new PersianCalendar();
                var convertedDateTime = Convert.ToDateTime(datetime);
                return pc.GetYear(convertedDateTime);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }
        public static int ToShamsiMonth(this DateTime datetime)
        {
            try
            {
                var pc = new PersianCalendar();
                var convertedDateTime = Convert.ToDateTime(datetime);
                var month = pc.GetMonth(convertedDateTime);

                return month;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }

        public static int ToShamsiSeason(this DateTime datetime)
        {
            try
            {
                var pc = new PersianCalendar();
                var convertedDateTime = Convert.ToDateTime(datetime);
                var month = pc.GetMonth(convertedDateTime);

                if (month <= 3)
                    return 1;
                else if (month <= 6 && month > 3)
                    return 2;
                else if (month <= 9 && month > 6)
                    return 3;
                else
                    return 4;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }
        public static int ToShamsiDay(this DateTime datetime)
        {
            try
            {
                var pc = new PersianCalendar();
                var convertedDateTime = Convert.ToDateTime(datetime);
                return pc.GetDayOfMonth(convertedDateTime);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return -1;
            }
        }
        public static DateTime? ToDateTime(this string shamsiDatetime)
        {
            try
            {
                if (shamsiDatetime == null)
                    return null;
                var pc = new PersianCalendar();
                var splited = shamsiDatetime
                    .Replace('۱', '1')
                    .Replace('۲', '2')
                    .Replace('۳', '3')
                    .Replace('۴', '4')
                    .Replace('۵', '5')
                    .Replace('۶', '6')
                    .Replace('۷', '7')
                    .Replace('۸', '8')
                    .Replace('۹', '9')
                    .Replace('۰', '0')

                    .Split('/').Select(int.Parse).ToArray();
                return pc.ToDateTime(splited[0], splited[1], splited[2], 0, 0, 0, 0);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
          where TAttribute : Attribute
        {
            try
            {
                return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }
        }

        public static DateTime FromShamsiDatetime(int year, int month, int day = 1)
        {
            try
            {
                var pc = new PersianCalendar();

                return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                throw;
            }
        }

        public static bool CanAddMonthlyReport(int year, int month)
        {
            try
            {
                var currentYear = DateTime.Now.ToShamsiYear();
                var currentMonth = DateTime.Now.ToShamsiMonth();

                if (year == currentYear)
                {
                    if (month >= currentMonth)
                        return false;
                }
                if (year > currentYear)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return false;
            }
        }

        public static bool CanAddSeasonalReport(int year, int season)
        {
            try
            {
                var currentYear = DateTime.Now.ToShamsiYear();
                var currentSeason = DateTime.Now.ToShamsiSeason();

                if (year == currentYear)
                {
                    if (season >= currentSeason)
                        return false;
                }
                if (year > currentYear)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return false;
            }
        }
      
    }


}
