/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/16 10:28:49

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
    [Serializable]
    internal sealed class ActionDelegatedEventHandler<TEvent>
        :IEventHandler<TEvent> where TEvent:IEvent
    {
        #region 私有字段
        private readonly Action<TEvent> eventHanderDelegate;
        #endregion

        #region 构造函数
        public ActionDelegatedEventHandler(Action<TEvent> eventHanderDelegate)
        {
            this.eventHanderDelegate = eventHanderDelegate;
        }
        #endregion


        /// <summary>
        /// 处理给定的事件
        /// 事实上，它是调用你传递过来的委托的实例，注意，这块设计的很巧妙
        /// </summary>
        /// <param name="evt"></param>
        public void Handle(TEvent evt)
        {
            this.eventHanderDelegate(evt);
        }

        /// <summary>
        /// 获取一个<see cref="Boolean"/>值，该值表示当前对象是否与给定的类型相同的另一对象相等。
        /// </summary>
        /// <param name="other">需要比较的与当前对象类型相同的另一对象。</param>
        /// <returns>如果两者相等，则返回true，否则返回false。</returns>

        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if ((object)other == (object)null)
                return false;
            //使用Delegate.Equals方法判断两个委托是否是代理的同一方法
            ActionDelegatedEventHandler<TEvent> otherDelegate = other
                as ActionDelegatedEventHandler<TEvent>;
            // 使用Delegate.Equals方法判定两个委托是否是代理的同一方法。
            return Delegate.Equals(this.eventHanderDelegate, otherDelegate.eventHanderDelegate);
        }
        public override int GetHashCode()
        {
            return this.eventHanderDelegate.GetHashCode();
        }
    }
}
