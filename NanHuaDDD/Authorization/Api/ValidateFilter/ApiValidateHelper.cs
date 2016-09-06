/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/12 11:03:34

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Authorization.Api
{
    public class ApiValidateHelper
    {
        /// <summary>
        /// 生成秘文，并返回
        /// </summary>
        /// <returns></returns>
        public static string GenerateCipherText(NameValueCollection coll, string passKey)
        {
            var paramStr = new StringBuilder();
            var keys = new List<string>();

            #region 验证算法
            foreach (string param in coll.Keys)
            {
                if (!string.IsNullOrEmpty(param))
                {
                    keys.Add(param.ToLower());
                }

            }
            keys.Sort();
            foreach (string p in keys)
            {
                if (!string.IsNullOrEmpty(coll[p]))
                {
                    paramStr.Append(coll[p]);
                }
            }
            paramStr.Append(DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmm"));
            paramStr.Append(passKey);
            #endregion
            //Utility.EncryptString(paramStr.ToString(), Utility.EncryptorType.MD5)
            return "";
        }
    }
}
