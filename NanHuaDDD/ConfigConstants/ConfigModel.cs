/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/10 22:12:46

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.CacheConfigFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.ConfigConstants
{
    public class ConfigModel : IConfiger
    {
        #region 缓存提供Caching(Redis,memcachecache)
        /// <summary>
        /// 缓存提供Caching(runtimecache,memcachecache)
        /// </summary>
        [DisplayName("缓存提供Caching(runtimecache,memcachecache)")]
        public string Cache_Provider { get; set; }
        /// <summary>
        /// 缓存过期时间
        /// </summary>
        [DisplayName("缓存过期时间")]
        public int Cache_ExpireMinutes { get; set; }
        #endregion

        #region memcache 配置
        /// <summary>
        /// ip地址+端口，多台服务器请用逗号隔开 如：127.0.0.1:11211,192.168.1.100:11211
        /// </summary>
        [DisplayName("ip地址+端口，多台服务器请用逗号隔开 如：127.0.0.1:11211,192.168.1.100:11211")]
        public string MemcacheStr { get; set; }
        #endregion

        #region 日志文件配置
        /// <summary>
        /// 日志实现方式：NormalLogger,Log4Logger
        /// </summary>
        [DisplayName("日志实现方式：NormalLogger,Log4Logger,MongoDB")]
        public string Logger_Type { get; set; }
        /// <summary>
        /// 日志级别：DEBUG|INFO|WARN|ERROR|FATAL|OFF
        /// </summary>
        [DisplayName("日志级别：DEBUG|INFO|WARN|ERROR|FATAL|OFF")]
        public string Logger_Level { get; set; }
        #endregion

        #region 消息类
        #region 发邮件类
       [DisplayName("邮箱from address")]
        public string Email_Address { get; set; }
        [DisplayName("展示名字")]
        public string Email_DisplayName { get; set; }
        [DisplayName("邮箱服务器地址")]
        public string Email_Host { get; set; }
        [DisplayName("邮箱密码")]
        public string Email_Password { get; set; }
        [DisplayName("邮箱端口")]
        public int Email_Port { get; set; }
        [DisplayName("邮箱用户名")]
        public string Email_UserName { get; set; }
       
        #endregion
        #region 发送短信
        [DisplayName("短信接口地址")]
        public string SMSGateway { get; set; }
        [DisplayName("短信接口加密类型（MD5）")]
        public string SMSSignType { get; set; }
        [DisplayName("字符格式")]
        public string SMSCharset { get; set; }
        [DisplayName("SMSKey")]
        public string SMSKey { get; set; }
        #endregion
        #endregion

        #region 队列
        [DisplayName("MemoryQueue,FileQueue")]
        public string Queue_Type { get; set; }
        #endregion

        #region Ioc
        [DisplayName("UnityAdapterContainer,AutofacAdapterContainer")]
        public string IoCType { get; set; }
       
        #endregion
    }
}
