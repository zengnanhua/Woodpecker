/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 10:38:21

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.OnlinePay.WapAlipay
{

    /// <summary>
    /// 配置config文件读取类
    /// </summary>
    public class WebConfig
    {
        #region 获取webconfig中AppSettings设置的参数
        /// <summary>
        /// 获取webconfig中的参数
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strDefault"></param>
        /// <returns></returns>
        public static string GetWebConfig(string strKey, string strDefault)
        {
            if (System.Configuration.ConfigurationManager.AppSettings[strKey] == null)
            {
                return strDefault;
            }
            else
            {
                return System.Configuration.ConfigurationManager.AppSettings[strKey].ToString();
            }
        }
        #endregion
    }
}
