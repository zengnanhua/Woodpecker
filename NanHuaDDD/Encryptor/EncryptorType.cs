/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/12 11:09:30

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Encryptor
{
    /// <summary>
    /// 加密类型
    /// </summary>
   public enum  EncryptorType
    {
            /// <summary>
            /// DES加密
            /// </summary>
            DES,
            /// <summary>
            /// 3DES加密
            /// </summary>
            DES3,
            /// <summary>
            /// MD5加密
            /// </summary>
            MD5,
            /// <summary>
            /// Base64加密
            /// </summary>
            Base64,
            /// <summary>
            /// 加密方法
            /// </summary>
            SHA256,
            /// <summary>
            /// DES加密（自定义密钥）
            /// </summary>
            DESByEncryptKey,
      
    }
}
