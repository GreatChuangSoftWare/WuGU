using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public static class DecimalExt
    {
        /// <summary>
        /// 转换为货币形式,无￥标记
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string MoneyNum(this decimal Num)
        {
            return string.Format("{0:N}", Num);
        }

        /// <summary>
        /// 转换为货币形式，无￥标记
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string MoneyNum(this decimal? Num)
        {
            return Num == null ? "" : string.Format("{0:N}", Num);
        }

        /// <summary>
        /// 转换为货币形式，有￥标记
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string MoneyNumY(this decimal? Num)
        {
            //string.Format("{0:C2}"
            return Num == null ? "<span class='text-left'>￥</span><span class='text-right'>0.00</span>" : string.Format("<span class='text-left'>￥</span><span class='text-right'>{0:N}</span>", Num);
        }

        //public static int MoneyNumtoCap(this decimal? dateA, DateTime? dateB)

        /// <summary>
        /// 转换为大写
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string MoneyNumtoCap(this decimal? Num)
        {
            if (Num == null)
                return "";
            var NumTemp = (decimal)Num;
            return NumTemp.MoneyNumtoCap();
        }

        /// <summary>
        /// 转换为大写
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string MoneyNumtoCap(this decimal Num)
        {
            string Capstr = "";
            string Cap = "零壹贰叁肆伍陆柒捌玖";
            string Numstr = "0123456789";
            string MoneyNumstr = Num.ToString();
            int Pint = MoneyNumstr.IndexOf(".");
            int Numint;

            string Moneyint = null;
            string Moneydec = null;
            string Intstr = null;
            string intstrend = null;
            string MoneyCap = null;
            string Moneyintstr = null;
            string Moneydecstr = null;
            // Capstr=Pint.ToString();

            if (Pint != -1)
            {
                string strArr = ".";
                char[] charArr = strArr.ToCharArray();
                string[] MoneyNumArr = MoneyNumstr.Split(charArr);
                Moneyint = MoneyNumArr[0].ToString();
                Moneydec = MoneyNumArr[1].ToString();

            }
            else
            {
                Moneyint = MoneyNumstr;
                Moneydec = "00";
            }

            if (Moneyint.Length > 16)
            {
                //MessageBox.Show("数值超界");
            }
            else
            {
                //--- 处理整数部分--------

                for (int j = 1; j <= Moneyint.Length; j++)
                {
                    Moneyintstr = Moneyint.Substring(j - 1, 1);
                    intstrend = Moneyint.Substring(j - 1);
                    if (Convert.ToInt32(intstrend) == 0)
                    {
                        Capstr = Capstr + "元";
                        break;
                    }
                    else
                    {
                        for (int i = 0; i <= 9; i++)
                        {
                            Intstr = Numstr.Substring(i, 1);
                            MoneyCap = Cap.Substring(i, 1);

                            if (Moneyintstr == Intstr)
                            {
                                #region
                                switch (Intstr)
                                {
                                    case "0":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "1":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "2":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "3":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "4":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "5":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "6":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "7":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "8":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "9":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                }
                                #endregion
                            }
                            //if (Convert.ToInt32(intstrend) == 0)
                            //    break;
                        }
                        Numint = Moneyint.Length - j + 1;
                        #region
                        switch (Numint)
                        {
                            case 16:
                                Capstr = Capstr + "仟万";
                                break;
                            case 15:
                                Capstr = Capstr + "佰万";
                                break;
                            case 14:
                                Capstr = Capstr + "拾万";
                                break;
                            case 13:
                                Capstr = Capstr + "万";
                                break;

                            case 12:
                                Capstr = Capstr + "仟";
                                break;

                            case 11:
                                Capstr = Capstr + "佰";
                                break;

                            case 10:
                                Capstr = Capstr + "拾";
                                break;

                            case 9:
                                Capstr = Capstr + "亿";
                                break;

                            case 8:
                                Capstr = Capstr + "仟";
                                break;

                            case 7:
                                Capstr = Capstr + "佰";
                                break;

                            case 6:
                                Capstr = Capstr + "拾";
                                break;


                            case 5:
                                Capstr = Capstr + "万";
                                break;

                            case 4:
                                Capstr = Capstr + "仟";
                                break;
                            case 3:
                                Capstr = Capstr + "佰";
                                break;
                            case 2:
                                Capstr = Capstr + "拾";
                                break;
                            //case 1:
                            //    Capstr = Capstr + "元";
                            //    break;
                        }
                        #endregion
                    }
                }

                //------处理小数部分－－－－－
                if (Convert.ToInt32(Moneydec) == 0)
                {
                    Capstr += "整";
                }
                else
                {
                    #region 小数
                    for (int j = 1; j <= 2; j++)
                    {
                        Moneydecstr = Moneydec.Substring(j - 1, 1);

                        for (int i = 0; i <= 9; i++)
                        {
                            Intstr = Numstr.Substring(i, 1);
                            MoneyCap = Cap.Substring(i, 1);
                            if (Moneydecstr == Intstr)
                            {

                                switch (Intstr)
                                {
                                    case "0":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "1":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "2":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "3":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "4":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "5":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "6":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "7":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "8":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                    case "9":
                                        Capstr = Capstr + MoneyCap;
                                        break;
                                }
                            }
                        }


                        switch (j)
                        {
                            case 1:
                                Capstr = Capstr + "角";
                                break;
                            case 2:
                                Capstr = Capstr + "分";
                                break;
                        }
                    }
                    #endregion
                }
            }

            return Capstr;
        }
    }
}
