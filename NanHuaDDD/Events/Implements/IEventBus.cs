using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Events.Implements
{
    /// <summary>
    /// 事件总线，产生者接口
    /// </summary>
    public interface IEventBus
    {

        #region 发布事件方法
        /// <summary>
        /// 发布事件，支持异步事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        void Publish<TEvent>(TEvent @event) where TEvent : class,IEvent;
        
        /// <summary>
        /// 发布事件
        /// event参数为关键字，所以加了@符
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        /// <param name="callback"></param>
        /// <param name="timeout"></param>
        void Publish<TEvent>(TEvent @event, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null) where TEvent : class,IEvent;
        
        /// <summary>
        /// 显示的异步发布事件，不需要为处理程序加 HandlesAsynchronouslyAttribute
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        void PublishAsync<TEvent>(TEvent @event) where TEvent : class,IEvent;

        /// <summary>
        /// 显式的异步发布事件,不需要为处理程序加HandlesAsynchronouslyAttribute
        /// event参数为关键字,所以加了@符
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        /// <param name="callback"></param>
        /// <param name="timeout"></param>
        void PublishAsync<TEvent>(TEvent @event, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null) where TEvent : class,IEvent;

        #endregion


        #region 订阅事件方法

        /// <summary>
        /// 订阅事件列表
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHandler"></param>
        void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class,IEvent;

        /// <summary>
        /// 订阅事件实体
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHandlerFunc"></param>
        void Subscribe<TEvent>(Action<TEvent> eventHandlerFunc)
            where TEvent : class,IEvent;

        /// <summary>
        /// 订阅事件集合
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHanders"></param>
        void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHanders) where
            TEvent : class,IEvent;
        #endregion


        #region 取消订阅
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHander"></param>
        void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHander) where
            TEvent : class,IEvent;

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHanderFunc"></param>
        void Unsubscribe<TEvent>(Action<TEvent> eventHanderFunc) where TEvent : class,IEvent;

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHanders"></param>
        void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHanders) where TEvent : class,IEvent;
        /// <summary>
        /// 取消全部订阅
        /// </summary>
        void UnsubscribeAll();
        /// <summary>
        /// 取消订阅全部事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        void UnsubscribeAll<TEvent>() where TEvent : class,IEvent;
        #endregion
    }
}
