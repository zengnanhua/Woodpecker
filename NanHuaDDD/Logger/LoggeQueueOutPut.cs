/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 17:40:50

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NanHuaDDD.Logger
{
    public class LoggeQueueOutPut
    {
         //异常信息的队列
        public static Queue<string> ExcMsg;

        static LoggeQueueOutPut()
        {
            ExcMsg=new Queue<string>();
            ThreadPool.QueueUserWorkItem(u =>
                {
                    while (true)
                    {
                        string str = string.Empty;

                        if (ExcMsg == null)
                        {
                            continue;
                        }

                        lock (ExcMsg)
                        {
                            if(ExcMsg.Count>0)
                            str = ExcMsg.Dequeue();
                        }
                        //往日志文件里面写就可以了。
                        if (!string.IsNullOrEmpty(str))
                        {
                            LoggerFactory.Instance.Logger_Debug(str);
                        }
                        if (ExcMsg.Count() <= 0)
                        {
                            Thread.Sleep(30);
                        }
                    }
                });
        }
        public static void WriteLog(string msg)
        {
            lock (ExcMsg)
            {
                ExcMsg.Enqueue(msg);
            }
        }
    }
}
