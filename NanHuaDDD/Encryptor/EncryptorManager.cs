﻿/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/12 12:40:13

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
    public class EncryptorManager
    {
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <returns></returns>
        public static string EncryptString(string str)
        {
            return EncryptString(str, 0, EncryptorType.DES, null);
        }
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <param name="code">加密长度，只有MD5加密有本参数</param>
        /// <param name="type">产生密码类型</param>
        /// <param name="passKey">密钥</param>
        /// <returns></returns>
        public static string EncryptString(string str, int code, EncryptorType type, string passKey)
        {
            string _tempString = str;
            switch (type)
            {
                case EncryptorType.DES:
                    _tempString = new DESEncryptor().EncryptString(str);
                    break;
                case EncryptorType.DES3:
                    break;
                case EncryptorType.MD5:
                    _tempString = MD5Encryptor.MD5(str, code);
                    break;
                case EncryptorType.Base64:
                    _tempString = new Base64Encryptor().EncryptString(str);
                    break;
                case EncryptorType.SHA256:
                    _tempString = SHA256Encryptor.SHA256(str);
                    break;
                case EncryptorType.DESByEncryptKey:
                    _tempString = DESByEncryptKey.EncryptDES(str, passKey);
                    break;
                default:
                    throw new ArgumentException("无效的加密类型");
            }
            return _tempString;
        }
        /// <summary>
        /// 加密方法(32位)
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <param name="type">产生密码类型</param>
        /// <returns></returns>
        public static string EncryptString(string str, EncryptorType type)
        {
            return EncryptString(str, 32, type, null);
        }

        /// <summary>
        /// 加密方法,带密钥
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <param name="passKey">密钥</param>
        /// <returns></returns>
        public static string EncryptString(string str, string passKey)
        {
            return EncryptString(str, 32, EncryptorType.DESByEncryptKey, passKey);
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="str">解密字符</param>
        /// <returns></returns>
        public static string DecryptString(string str)
        {
            return DecryptString(str, EncryptorType.DES, null);
        }

        /// <summary>
        /// 解密方法,带密钥
        /// </summary>
        /// <param name="str">需解密字符</param>
        /// <param name="passKey">密钥</param>
        /// <returns></returns>
        public static string DecryptString(string str, string passKey)
        {
            return DecryptString(str, EncryptorType.DESByEncryptKey, passKey);
        }

        public static string DecryptString(string str, EncryptorType type)
        {
            return DecryptString(str, type, null);
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="str">需解密字符</param>
        /// <param name="type">解密方法</param>
        /// <returns></returns>
        public static string DecryptString(string str, EncryptorType type, string passKey)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            string _tempString = str;
            switch (type)
            {
                case EncryptorType.DES:
                    _tempString = new DESEncryptor().DecryptString(str);
                    break;
                case EncryptorType.DES3:
                    break;
                case EncryptorType.Base64:
                    _tempString = new Base64Encryptor().DecryptString(str);
                    break;
                case EncryptorType.DESByEncryptKey:
                    _tempString = DESByEncryptKey.DecryptDES(str, passKey);
                    break;
                default:
                    throw new ArgumentException("无效的加密类型");
            }

            return _tempString;
        }
    }
}
