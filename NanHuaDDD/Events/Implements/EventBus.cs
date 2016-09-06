/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/16 11:11:57

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Events.Implements
{

   

    /// <summary>
    /// 事件总线【事件功能核心代码】
    /// </summary>
    public class EventBus:IEventBus
    {
        #region 私有字段

        private static object _objLock = new object();
        /// <summary>
        /// 事件总线对象
        /// </summary>
        private static EventBus _instance = null;
        /// <summary>
        /// 事件总线存储方式
        /// </summary>
        private static string _eventBusType = "MemoryEventBus";
        /// <summary>
        /// 事件生产者
        /// </summary>
        private IEventBus _iEventBus = null;
        #endregion
        private EventBus()
        {
            if (_eventBusType.ToLower() == "memoryeventbus")
                _iEventBus = new MemoryEventBus();
        }

        #region 实例方法
        /// <summary>
        /// 初始化空的事件总件,单例模式,双重锁，解决并发和性能问题
        /// </summary>
        public static IEventBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_objLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new EventBus();
                        }
                    }
                }
                return _instance;
            }
        }


        #endregion

        #region 发布事件
        public void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            _iEventBus.Publish<TEvent>(@event);
        }

        public void Publish<TEvent>(TEvent @event, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null) where TEvent : class, IEvent
        {
            _iEventBus.Publish<TEvent>(@event,callback,timeout);
        }

        public void PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            _iEventBus.PublishAsync<TEvent>(@event);
        }

        public void PublishAsync<TEvent>(TEvent @event, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null) where TEvent : class, IEvent
        {
            _iEventBus.PublishAsync<TEvent>(@event, callback, timeout);
        }
        #endregion

        #region 订阅事件
        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : class, IEvent
        {
            _iEventBus.Subscribe<TEvent>(eventHandler);
        }

        public void Subscribe<TEvent>(Action<TEvent> eventHandlerFunc) where TEvent : class, IEvent
        {
            _iEventBus.Subscribe<TEvent>(eventHandlerFunc);
        }

        public void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHanders) where TEvent : class, IEvent
        {
            _iEventBus.Subscribe<TEvent>(eventHanders);
        }
        #endregion

        #region 取消订阅
        public void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHander) where TEvent : class, IEvent
        {
            _iEventBus.Unsubscribe<TEvent>(eventHander);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> eventHanderFunc) where TEvent : class, IEvent
        {
            _iEventBus.Unsubscribe<TEvent>(eventHanderFunc);
        }

        public void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHanders) where TEvent : class, IEvent
        {
            _iEventBus.Unsubscribe<TEvent>(eventHanders);
        }

        public void UnsubscribeAll()
        {
            _iEventBus.UnsubscribeAll();
        }

        public void UnsubscribeAll<TEvent>() where TEvent : class, IEvent
        {
            _iEventBus.UnsubscribeAll<TEvent>();
        }
        #endregion
    }
}
