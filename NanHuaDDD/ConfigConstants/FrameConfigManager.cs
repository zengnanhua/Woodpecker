/************************************************************************
 * 
*创建标记：啄木鸟

*创建时间：2016/5/10 22:26:07

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.CacheConfigFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.ConfigConstants
{
    public class FrameConfigManager
    {
        static ConfigModel configMode = InitConfig();
        public static ConfigModel Config {
            get {
                //ConfigModel configModel = ConfigFactory.Instance.GetConfig<ConfigModel>();
                return configMode;
                //return configModel;
            }
        }

        public static ConfigModel InitConfig()
        {
            var entity=new ConfigModel();
            #region 缓存提供
            entity.Cache_Provider = "MemcacheCache";
            entity.Cache_ExpireMinutes = 20;
           
            #endregion
            #region  memcache 配置
            entity.MemcacheStr = "127.0.0.1:11211";
            #endregion

            #region 日志文件配置
            entity.Logger_Type = "NormalLogger";
            entity.Logger_Level = "DEBUG";
            #endregion

            #region 消息类
            #region 邮件配置
            entity.Email_Address = "418852693@qq.com";
            entity.Email_DisplayName = "曾南华";
            entity.Email_Host = "smtp.qq.com";
            entity.Email_Password = "123456";
            entity.Email_Port = 25;
            entity.Email_UserName = "418852693@qq.com";
            #endregion
            #region 短信配置
            entity.SMSGateway = "http://sms.yourname.com/Msg/SendMessage";
            entity.SMSCharset = "MD5";
            entity.SMSCharset = "utf-8";
            entity.SMSKey = "04fa25475e07669d4809d334f08fb35b";
           
            #endregion
            #endregion

            #region 队列
            entity.Queue_Type = "filequeue";
            #endregion

            #region Ioc
            entity.IoCType = "AutofacAdapterContainer";
            #endregion
            return entity;
        }
    }
}
