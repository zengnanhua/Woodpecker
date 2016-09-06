/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 16:18:39

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Logger
{
    public interface ILogger
    {
        #region 功能日志
        /// <summary>
        /// 记录代码运行的时间，日志文件名以codeTime开头的时间戳
        /// </summary>
        /// <param name="message"></param>
        /// <param name="action"></param>
        void Logger_Timer(string message, Action action);
        /// <summary>
        /// 记录代码运行异常，日志文件名以Exception开头的时间戳
        /// </summary>
        /// <param name="message"></param>
        /// <param name="action"></param>
        void Logger_Exception(string message, Action action);

        #endregion

        #region 日志级别
        /// <summary>
        /// 将message 记录到日志文件
        /// </summary>
        /// <param name="message"></param>
        void Logger_info(string message);
        /// <summary>
        /// 异常发生的日志
        /// </summary>
        /// <param name="ex"></param>
        void Logger_Error(Exception ex);
        /// <summary>
        /// 调试期间的日志
        /// </summary>
        /// <param name="message"></param>
        void Logger_Debug(string message);
        /// <summary>
        /// 引起程序终止的日志
        /// </summary>
        /// <param name="message"></param>
        void Logger_Fatal(string message);
        /// <summary>
        /// 引起警告的日志
        /// </summary>
        /// <param name="message"></param>
        void Logger_Warn(string message);
        #endregion
    }
}
