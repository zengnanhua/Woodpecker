/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 16:52:28

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Logger.Implements
{
    class Log4Logger:LoggerBase
    {

        /// <summary>
        /// log4net配置文件路径
        /// </summary>
        static string _logConfig = string.Empty;
        /// <summary>
        /// log4net日志执行者
        /// </summary>
        static log4net.Core.LogImpl logImpl;
        /// <summary>
        /// 私有架造方法
        /// </summary>

        static Log4Logger()
        {
            try
            {
                _logConfig = System.Web.HttpContext.Current.Server.MapPath("/Configs/log4net.config");
            }
            catch (NullReferenceException)
            {
                try
                {
                    _logConfig = System.Web.HttpRuntime.AppDomainAppPath + "\\Configs\\log4net.config"; //例如：c:\\project\\
                }
                catch (ArgumentNullException)
                {

                    _logConfig = Environment.CurrentDirectory + "\\Configs\\log4net.config"; //例如：c:\\project\\bin\\debug
                }

            }
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(_logConfig));//初始化指定配置文件
            //log4net.Config.XmlConfigurator.Configure();//初始化web.config里的log4net节点

            logImpl = log4net.LogManager.GetLogger("Core.Logger") as log4net.Core.LogImpl;//Core.Logger为log4net方案名称,为空表示使用root节点
        }

        protected override void InputLogger(string message)
        {
            logImpl.Info(message);
        }
          public override void Logger_Error(Exception ex)
        {
            logImpl.Error(ex);
        }

        public override void Logger_Debug(string message)
        {
            logImpl.Debug(message);
        }

        public override void Logger_Fatal(string message)
        {
            logImpl.Fatal(message);
        }

        public override void Logger_info(string message)
        {
            logImpl.Info(message);
        }

        public override void Logger_Warn(string message)
        {
            logImpl.Warn(message);
        }

    }
}
