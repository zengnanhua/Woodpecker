using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.CachingQueue
{
    /// <summary>
    /// 队列标准
    /// </summary>
    interface IQueue
    {
        /// <summary>
        /// 添加到队列
        /// </summary>
        /// <param name="obj"></param>
        void Push(byte[] obj);
        /// <summary>
        /// 从队列中取出
        /// </summary>
        /// <returns></returns>
        byte[] Pop();
        /// <summary>
        /// 得到队列的项目总数
        /// </summary>
        /// <returns></returns>
        int Count { get; }
    }
}
