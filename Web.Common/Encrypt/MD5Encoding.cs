using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

namespace Web.Common
{
    public class MD5Encoding
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string EncodingStr(string str)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] pwd = System.Text.Encoding.Default.GetBytes(str);
            byte[] NewPwd = md5.ComputeHash(pwd);
            md5.Clear();

            foreach (byte item in NewPwd)
            {
                sb.Append(item.ToString("X2"));
            }
            return sb.ToString();
        }

    }
}
