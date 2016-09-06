/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 23:03:38

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.CachingQueue.Implements
{
    internal class MemoryQueue:IQueue
    {
        private static readonly Queue<byte[]> _queue = new Queue<byte[]>();
        public void Push(byte[] obj)
        {
            _queue.Enqueue(obj);
        }

        public byte[] Pop()
        {
            if (this.Count > 0)
                return _queue.Dequeue();
            return null;
        }

        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }
    }
}
