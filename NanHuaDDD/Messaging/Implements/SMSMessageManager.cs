/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 21:36:18

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.ConfigConstants;
using NanHuaDDD.Singleton;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NanHuaDDD.Messaging.Implements
{
    internal class SMSMessageManager : Singleton<SMSMessageManager>,IMessageManager
    {
        #region 私有字段
        private string gateway = FrameConfigManager.Config.SMSGateway??"http://sms.yourname.com/Msg/SendMessage";
        private string sign_type = FrameConfigManager.Config.SMSCharset ?? "MD5";
        private string input_charset = FrameConfigManager.Config.SMSCharset ?? "utf-8";
        private string key = FrameConfigManager.Config.SMSKey ?? "04fa25475e07669d4809d334f08fb35b";
        int itemID = 1008;
        #endregion 
        private SMSMessageManager()
        { }
       
        #region 接口函数
        public void Send(string recipient, string subject, string body, string serverVirtualPath = null)
        {
            Send(new List<string> { recipient }, subject, body);
        }

        public void Send(IEnumerable<string> recipients, string subject, string body, string serverVirtualPath = null)
        {
            Send(recipients, subject, body, false);
        }

        public void Send(IEnumerable<string> recipients, string subject, string body, bool isAsync, string serverVirtualPath = null)
        {
            try
            {
                var taskID = new Random().Next(1, int.MaxValue);
                var channel = 3;
                recipients = recipients.Where(i => new Regex(@"^1[3|4|5|8|7|6][0-9]\d{8}$").IsMatch(i));
                var mobile=string.Join(";",recipients);
                if (!string.IsNullOrWhiteSpace(mobile))
                {
                    var content = string.Concat(subject, ",", body, "【大叔消息机制】");
                    var para = new String[] { 
                         "itemID=" + itemID.ToString(),
                                             "taskID=" + taskID.ToString(),
                                             "channel="+channel.ToString(),
                                             "mobile=" + mobile,
                                             "contents=" + content,
                                             "sendTime=" + DateTime.Now.ToString(),
                                             "_input_charset=" + input_charset
                    };
                    var preValues = string.Empty;
                    var sign = CreatSign(para, input_charset, key, out preValues);
                    var str = "";
                    for (int i = 0; i < para.Length; i++)
                    {
                        if (para[i].Contains("contents"))//对内容部分做 Server.urlencode
                        {
                            str += "contents=" + System.Web.HttpUtility.UrlEncode(content) + "&";
                        }
                        else
                            str += para[i] + "&";
                    }
                    str += "sign=" + sign;
                    if (isAsync)
                    {
                        Task.Run(() =>
                        {
                            PostMethod(str, gateway, "POST", Encoding.GetEncoding("UTF-8"));
                        });
                    }
                    else {
                        string res = PostMethod(str, gateway, "POST", Encoding.GetEncoding("UTF-8"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 生成校验字符串
        /// </summary>
        /// <param name="para">参数加密数组</param>
        /// <param name="_input_charset">编码格式</param>
        /// <param name="key">安全校验码</param>
        /// <param name="preValues">返回加密前数据</param>
        /// <returns>字符串URL或加密结果</returns>
        static string CreatSign(
          string[] para,
          string _input_charset,
          string key,
          out string preValues
          )
        {
            //进行排序；
            string[] Sortedstr = BubbleSort(para);
            //构造待md5摘要字符串 ；
            string paraStr = string.Join("&", Sortedstr) + key;
            //生成Md5摘要；
            string sign = GetMD5(paraStr, _input_charset);
            preValues = paraStr;
            return sign;
        }

        /// <summary>
        /// 冒泡排序法
        /// 按照字母序列从a到z的顺序排列
        /// </summary>
        static string[] BubbleSort(string[] r)
        {
            int i, j; //交换标志 
            string temp;

            bool exchange;

            for (i = 0; i < r.Length; i++) //最多做R.Length-1趟排序 
            {
                exchange = false; //本趟排序开始前，交换标志应为假

                for (j = r.Length - 2; j >= i; j--)
                {//交换条件
                    if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;

                        exchange = true; //发生了交换，故将交换标志置为真 
                    }
                }

                if (!exchange) //本趟排序未发生交换，提前终止算法 
                {
                    break;
                }
            }
            return r;
        }
        /// <summary>
        /// 与ASP兼容的MD5加密算法
        /// </summary>
        static string GetMD5(string s, string _input_charset)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 简单的HTTP请求
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="postUrl"></param>
        /// <param name="method"></param>
        /// <param name="encoder"></param>
        /// <returns></returns>
        private string PostMethod(string postData, string postUrl, string method, Encoding encoder)
        {
            try
            {
                if (method == "GET")
                {
                    if (!postUrl.Contains("?"))
                        postUrl = postUrl + "?";

                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(postUrl + postData);
                    req.Method = method;
                    req.Timeout = 3 * 1000;
                    HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), encoder);
                    return reader.ReadToEnd();
                }
                else if (method == "POST")
                {
                    HttpWebRequest rqt = (HttpWebRequest)HttpWebRequest.Create(postUrl);
                    rqt.Method = method;
                    rqt.Timeout = 3 * 1000;
                    rqt.ContentType = "application/x-www-form-urlencoded";
                    byte[] postdata = encoder.GetBytes(postData);
                    rqt.ContentLength = postdata.Length;
                    Stream writer = rqt.GetRequestStream();
                    writer.Write(postdata, 0, postdata.Length);
                    HttpWebResponse reps = (HttpWebResponse)rqt.GetResponse();

                    HttpWebResponse myResponse = (HttpWebResponse)rqt.GetResponse();
                    StreamReader reader = new StreamReader(myResponse.GetResponseStream(), encoder);
                    return reader.ReadToEnd();
                }
                else
                {
                    return "method参数不对";
                }
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }
        #endregion
    }
}
