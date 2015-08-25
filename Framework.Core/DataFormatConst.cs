using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class DataFormatConst
    {
        public const string DecimalDigits0 = "{0:0}";
        public const string DecimalDigits1 = "{0:0.0}";
        public const string DecimalDigits2 = "{0:0.00}";
        public const string DecimalDigits3 = "{0:0.000}";
        public const string DecimalDigits4 = "{0:0.0000}";

        public const string DateTimeyyyy_MM_dd = "yyyy-MM-dd";
        public const string DateTimeyyyy_MM = "yyyy-MM";
        public const string DateTimeyyyyMMdd = "yyyyMMdd";
        public const string DateTimeyyyyMM = "yyyyMM";

        public static string DateTimeFormat(DateTime? datetime, string strFormat = DataFormatConst.DateTimeyyyy_MM_dd)
        {
            return datetime == null ? "" : ((DateTime)datetime).ToString(strFormat);
        }

        public static string DecimalFormat(Decimal? dec, string strFormat = DataFormatConst.DecimalDigits2)
        {
            return String.Format("{0:" + strFormat + "}", dec);
        }
    }
}
