/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/16 10:24:20

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Events
{
    /// <summary>
    /// 事件对象【实体核心】
    /// </summary>
    public class EventBase:IEvent
    {
        /// <summary>
        /// 获取事实范围内的唯一标识，生命周期在本事件会话内有效
        /// </summary>
        public Guid AggregateRoot
        {
            get { return Guid.NewGuid(); }
        }

        /// <summary>
        /// 事件发生的时间
        /// </summary>
        public DateTime EventDate
        {
            get { return DateTime.Now; }
        }

    }
}
