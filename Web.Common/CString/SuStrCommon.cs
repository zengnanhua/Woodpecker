using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common
{
    /// <summary>
    /// 截取字符串长度
    /// </summary>
    public static class SuStrCommon
    {
        #region 截取字符串,默认截图长度是100
        /// <summary>
        /// 截取字符串,默认截图长度是100
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length">默认是100</param>
        /// <returns></returns>
        public static string SubString(this string str, int length = 100)
        {
            if (length == 100)
            {
                if (str.Length > length)
                {
                    return str.Substring(0, 100) + "...";
                }
                else
                {
                    return str;
                }
            }
            else
            {
                if (str.Length > length)
                {
                    return str.Substring(0, length) + "...";
                }
                return str;

            }


        }

        public static string AftSubString(this string str, int length = 100)
        {
            if (length == 100)
            {
                if (str.Length > length)
                {
                    //return str.Substring(0, 100) + "...";
                    string aa = "..." + str.Substring(str.Length - 100, length);
                    return aa;
                }
                else
                {
                    return str;
                }
            }
            else
            {
                if (str.Length > length)
                {
                    return "..." + str.Substring(str.Length, length);
                }
                return str;

            }


        }

        #endregion

    }
}
