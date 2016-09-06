/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/16 10:18:48

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
    /// 领域事件实体基类【实体接口】
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// 领域事件实体的聚合根，生命周期在会话结束后消失
        /// </summary>
        Guid AggregateRoot { get; }
    }
}
