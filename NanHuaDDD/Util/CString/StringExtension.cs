/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/17 10:24:51

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Util.CString
{
    public static class StringExtension
    {
        #region 比较字符串是否相等
        public static bool IsSame(this string strObj, string strCompare)
        {
            if (string.IsNullOrEmpty(strObj) || string.IsNullOrEmpty(strCompare))
                return false;
            return string.Equals(strObj, strCompare, StringComparison.CurrentCultureIgnoreCase);
        }
        #endregion
        #region 3.0 判断当前字符串是否为空 + bool IsNullOrEmpty(this string strOri)
        /// <summary>
        /// 3.0 判断当前字符串是否为空
        /// </summary>
        /// <param name="strOri">源字符串</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string strOri)
        {
            return string.IsNullOrEmpty(strOri);
        }
        #endregion
    }
}
