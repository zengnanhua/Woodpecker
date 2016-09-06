/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/16 15:23:10

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace NanHuaDDD.Util.CString
{
    public static class StringFormsAuthentication
    {
        /// <summary>
        /// 1.0 将字符串加密
        /// </summary>
        /// <param name="strOri"></param>
        /// <returns></returns>
        public static string ToEncyptFormsAuthenticationString(this string strOri)
        {
            //1.创建 授权票据对象，将 要加密的字符串 传入
            FormsAuthenticationTicket ticket = new System.Web.Security.FormsAuthenticationTicket(1, "aa", DateTime.Now, DateTime.Now.AddMinutes(60), true, strOri);
            //2.调用加密方法，将票据 转成 加密字符串返回
            return FormsAuthentication.Encrypt(ticket);
        }

        /// <summary>
        /// 2.0 解密字符串
        /// </summary>
        /// <param name="strOri"></param>
        /// <returns></returns>
        public static string ToDecyptFormsAuthenticationString(this string strOri)
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(strOri);
            return ticket.UserData;
        }
    }
}
