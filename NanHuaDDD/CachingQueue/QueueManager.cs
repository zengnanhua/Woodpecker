/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 23:09:09

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.CachingQueue.Implements;
using NanHuaDDD.ConfigConstants;
using NanHuaDDD.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.CachingQueue
{
    public class QueueManager : Singleton<QueueManager>, IQueue
    {
        string queueType = FrameConfigManager.Config.Queue_Type ?? "MemoryQueue";
        private IQueue _iQueue;
        private QueueManager()
        {
            switch (queueType.ToLower())
            {
                case "memoryqueue":
                    _iQueue = new MemoryQueue();
                    break;
                case "filequeue":
                    _iQueue = new FileQueue();
                    break;
                default:
                    throw new ArgumentException("队列存储方式不正确目前只支持Default,File");
            }
        }

        public void Push(byte[] obj)
        {
            _iQueue.Push(obj);
        }

        public byte[] Pop()
        {
            return _iQueue.Pop();
        }

        public int Count
        {
            get { return _iQueue.Count; }
        }
    }
}
