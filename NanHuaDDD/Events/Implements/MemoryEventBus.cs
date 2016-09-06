/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/16 11:15:30

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
    class MemoryEventBus:IEventBus
    {
        /// <summary>
        /// 模式锁
        /// </summary>
        private static object _objLock = new object();
        /// <summary>
        /// 对于事件数据的存储，目前采用内存字典
        /// </summary>
        private static Dictionary<Type, List<object>> _eventHandlers = new Dictionary<Type, List<object>>();

        private readonly Func<object, object, bool> _eventHandlerEquals = (o1, o2) => {
            var o1Type = o1.GetType();
            var o2Type = o2.GetType();
            if (o1Type.IsGenericType && o1Type.GetGenericTypeDefinition() == typeof(ActionDelegatedEventHandler<>) && o2Type.IsGenericType && o2Type.GetGenericTypeDefinition() == typeof(ActionDelegatedEventHandler<>))
                return o1.Equals(o2);
            return o1Type == o2Type;
            
        };

        #region 发布事件
        public void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            if (@event == null)
            {
                throw new ArgumentNullException("@event");
            }
            var eventType = @event.GetType();
            if (_eventHandlers.ContainsKey(eventType) && _eventHandlers[eventType] != null && _eventHandlers[eventType].Count > 0)
            {
                var handlers = _eventHandlers[eventType];

                foreach (var handler in handlers)
                {
                    var eventHandler = handler as IEventHandler<TEvent>;
                    if (eventHandler.GetType().IsDefined(typeof(HandlesAsynchronouslyAttribute), false))
                    {
                        Task.Factory.StartNew((o)=>eventHandler.Handle((TEvent)o),@event);
                    }
                    else {
                        eventHandler.Handle(@event);
                    }
                }

            }
        }

        public void Publish<TEvent>(TEvent @event, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null) where TEvent : class, IEvent
        {
            if (@event == null)
                throw new ArgumentNullException("@event");
            var eventType = @event.GetType();
            if (_eventHandlers.ContainsKey(eventType) && _eventHandlers[eventType] != null && _eventHandlers[eventType].Count > 0)
            {
                var handlers = _eventHandlers[eventType];
                List<Task> tasks = new List<Task>();
                try
                {
                    foreach (var handler in handlers)
                    {
                        var eventHandler = handler as IEventHandler<TEvent>;
                        if (eventHandler.GetType().IsDefined(typeof(HandlesAsynchronouslyAttribute), false))
                        {
                            tasks.Add(Task.Factory.StartNew((o) => eventHandler.Handle((TEvent)o), @event));
                        }
                        else
                        {
                            eventHandler.Handle(@event);
                        }
                    }
                    if (tasks.Count > 0)
                    {
                        if (timeout == null)
                        {
                            Task.WaitAll(tasks.ToArray());
                        }
                        else
                        {
                            Task.WaitAll(tasks.ToArray(), timeout.Value);
                        }
                    }
                    callback(@event, true, null);
                }
                catch (Exception ex)
                {
                    callback(@event, false, ex);
                }
            }
            else {
                callback(@event, false, null);
            }
        }

        public void PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            Task.Factory.StartNew(() => Publish(@event));
        }

        public void PublishAsync<TEvent>(TEvent @event, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null) where TEvent : class, IEvent
        {
            Task.Factory.StartNew(() => Publish(@event, callback, timeout));
        }
        #endregion

        #region 事件订阅
        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : class, IEvent
        {
            lock (_objLock)
            {
                var eventType = typeof(TEvent);
                if (_eventHandlers.ContainsKey(eventType))
                {
                    var handlers = _eventHandlers[eventType];
                    if (handlers != null)
                    {
                        if (!handlers.Exists(deh => _eventHandlerEquals(deh, eventHandler)))
                        {
                            handlers.Add(eventHandler);
                        }
                        else
                        {
                            handlers = new List<object>();
                            handlers.Add(eventHandler);
                        }
                    }
            
                }
                else
                {
                    _eventHandlers.Add(eventType, new List<object> { eventHandler });
                }
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> eventHandlerFunc) where TEvent : class, IEvent
        {
            Subscribe<TEvent>(new ActionDelegatedEventHandler<TEvent>(eventHandlerFunc));

        }
        public void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHanders) where TEvent : class, IEvent
        {
            foreach (var eventHandle in eventHanders)
            {
                Subscribe<TEvent>(eventHandle);
            }
        }
        #endregion
        #region 取消订阅
        public void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : class, IEvent
        {
            lock (_objLock)
            {
                var eventType = typeof(TEvent);
                if (_eventHandlers.ContainsKey(eventType))
                {
                    var handlers = _eventHandlers[eventType];
                    if (handlers != null && handlers.Exists(deh => _eventHandlerEquals(deh, eventHandler)))
                    {
                        var handlerToRemove = handlers.First(deh => _eventHandlerEquals(deh, eventHandler));
                        handlers.Remove(handlerToRemove);
                    }
                }
            }
        }

        public void Unsubscribe<TEvent>(Action<TEvent> eventHanderFunc) where TEvent : class, IEvent
        {
            Unsubscribe<TEvent>(new ActionDelegatedEventHandler<TEvent>(eventHanderFunc));
        }

        public void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHanders) where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHanders)
            {
                Unsubscribe<TEvent>(eventHandler);
            }
        }

        public void UnsubscribeAll()
        {
            lock (_objLock)
            {
                _eventHandlers.Clear();
            } 
        }

        public void UnsubscribeAll<TEvent>() where TEvent : class, IEvent
        {
            lock (_objLock)
            {
                var eventType = typeof(TEvent);
                if (_eventHandlers.ContainsKey(eventType))
                {
                    var handlers = _eventHandlers[eventType];
                    if (handlers != null)
                        handlers.Clear();
                }
            }
        }
        #endregion
    }
}
