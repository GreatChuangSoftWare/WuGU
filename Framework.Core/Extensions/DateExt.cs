using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public static class DateTimeExt
    {
        /// <summary>
        /// 日期间相差月份
        /// </summary>
        /// <param name="dateA"></param>
        /// <param name="dateB"></param>
        /// <returns></returns>
        public static int GetMonth(this DateTime? dateA, DateTime? dateB)
        {
            if (dateA == null || dateB == null)
            {
                return 0;
            }
            var date1 = (DateTime)dateA;
            var date2 = (DateTime)dateB;
            int year1 = date1.Year;
            int year2 = date2.Year;
            int month1 = date1.Month;
            int month2 = date2.Month;
            int months = 12 * (year2 - year1) + (month2 - month1);
            return months;
        }

        public static int GetDay(this DateTime? dateA, DateTime? dateB)
        {
            var dateATemp = Convert.ToDateTime(dateA);
            var dateBTemp = Convert.ToDateTime(dateB);
            var day = dateATemp.Subtract(dateBTemp);
            return day.Days;
        }


        /// <summary>
        /// 转换为：yyyy-MM-dd
        /// </summary>
        /// <param name="dateA"></param>
        /// <returns></returns>
        public static string Format_yyyy_MM_dd(this DateTime? dateA)
        {
            var strFmt = "";
            strFmt=dateA == null ? "" : Convert.ToDateTime(dateA).ToString("yyyy-MM-dd");
            return strFmt;
        }

        /// <summary>
        /// 转换为：yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="dateA"></param>
        /// <returns></returns>
        public static string Format_yyyy_MM_dd_HH_mm_ss(this DateTime? dateA)
        {
            var strFmt = "";
            strFmt = dateA == null ? "" : Convert.ToDateTime(dateA).ToString("yyyy-MM-dd HH:mm:ss");
            return strFmt;
        }
    }


}
