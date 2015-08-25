using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public static class StringExt
    {
        /// <summary>
        /// 字符串转换为日期类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDate(this string str)
        {
            DateTime dtime = new DateTime();
            DateTime.TryParse(str, out dtime);
            return dtime;
        }

        /// <summary>
        /// 字符串转换为日期类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToDateNull(this string str)
        {
            if (str == "" || str == null)
                return null;
            DateTime dtime = new DateTime();
            DateTime.TryParse(str, out dtime);
            return dtime;
        }

        /// <summary>
        /// 字符串转换为Decimal类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal ToDecimal(this string str)
        {
            Decimal dec = new Decimal();
            Decimal.TryParse(str, out dec);
            return dec;
        }

        /// <summary>
        /// 字符串转换为Decimal类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal? ToDecimalNull(this string str)
        {
            if (str == "" || str == null)
                return null;
            Decimal dec = new Decimal();
            Decimal.TryParse(str, out dec);
            return dec;
        }

    }
}
