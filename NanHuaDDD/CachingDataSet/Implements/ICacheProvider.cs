/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/17 10:35:55

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.CachingDataSet.Implements
{
    /// <summary>
    /// 表示实现该接口的类型是能够为应用程序提供缓存机制的类型
    /// </summary>
    public interface ICacheProvider
    {
        #region Methods
        /// <summary>
        /// 向缓存中添加一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生</param>
        /// <param name="value">需要缓存的对象</param>
        void Add(string key, string valKey, object value);

        /// <summary>
        /// 向缓存中更新一个对象。
        /// </summary>
        /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
        /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
        /// <param name="value">需要缓存的对象。</param>
        void Put(string key, string valKey, object value);

        object Get(string key, string valKey);

        void Remove(string key);
        void Remove(string key, string valKey);

        bool Exists(string key);

        bool Exists(string key, string valKey);
        #endregion
    }
}
