/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 16:48:29

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NanHuaDDD.Logger.Implements
{
    internal class NormalLogger : LoggerBase
    {
        static readonly object objLock = new object();
        protected override void InputLogger(string message)
        {
            string filePath = Path.Combine(FileUrl, DateTime.Now.ToLongDateString() + ".log");

            if (!System.IO.Directory.Exists(FileUrl))
                System.IO.Directory.CreateDirectory(FileUrl);
            lock (objLock)//防治多线程读写冲突
            {
                using (System.IO.StreamWriter srFile = new System.IO.StreamWriter(filePath, true))
                {
                    srFile.WriteLine(string.Format("{0}{1}{2}"
                        , DateTime.Now.ToString().PadRight(20)
                        , ("[ThreadID:" + Thread.CurrentThread.ManagedThreadId.ToString() + "]").PadRight(14)
                        , message));
                    srFile.Close();
                    srFile.Dispose();
                }
            }
        }
    }
}
