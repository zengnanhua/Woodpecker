/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 17:01:05

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.ConfigConstants;
using NanHuaDDD.Logger.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Logger
{
    public class LoggerFactory
    {

        #region 成员
        /// <summary>
        /// 日志提供者，只在本类中实例化
        /// </summary>
        private ILogger iLogger;
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object lockObj = new object();
        /// <summary>
        /// 日志工厂
        /// </summary>
        private static LoggerFactory instance;
        /// <summary>
        /// 日志级别
        /// </summary>
        private static LoggerLevel level = (LoggerLevel)Enum.Parse(typeof(LoggerLevel), (FrameConfigManager.Config.Logger_Level ?? "DEBUG").ToUpper());

        #endregion
        private LoggerFactory()
        {
            string type = "file";
            type = (ConfigConstants.FrameConfigManager.Config.Logger_Type ?? "log4net").ToLower();
           
            switch (type)
            {
                case "normallogger":
                    iLogger = new NormalLogger();
                    break;
                case "log4net":
                    iLogger = new Log4Logger();
                    break;
                default:
                    throw new ArgumentException("请正确配置AppSetting");
            }
        }
        public static LoggerFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new LoggerFactory();
                        }
                    }
                }
                return instance;
            }
        }


        #region ILogger 成员方法
        public void Logger_Timer(string message, Action action)
        {
            iLogger.Logger_Timer(message, action);
        }
        public void Logger_Exception(string message, Action action)
        {
            iLogger.Logger_Exception(message, action);
        }
        public void Logger_Debug(string message)
        {
            if (level <= LoggerLevel.DEBUG)
                iLogger.Logger_Debug(message);
        }
        public void Logger_Info(string message)
        {
            if (level <= LoggerLevel.INFO)
                iLogger.Logger_info(message);
        }
        public void Logger_Warn(string message)
        {
            if (level <= LoggerLevel.WARN)
                iLogger.Logger_Warn(message);
        }
        public void Logger_Error(Exception ex)
        {
            if (level <= LoggerLevel.ERROR)
                iLogger.Logger_Error(ex);
        }
        public void Logger_Fatal(string message)
        {
            if (level <= LoggerLevel.FATAL)
                iLogger.Logger_Fatal(message);
        }
        #endregion
    }
}
