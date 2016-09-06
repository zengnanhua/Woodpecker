/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/17 10:54:13

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.CachingDataSet.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.CachingDataSet
{
    public sealed class CacheDataSetManager : ICacheProvider
    {
        #region Private Fields
        private readonly ICacheProvider _cacheProvider;
        private static readonly CacheDataSetManager _instance;
        #endregion
                #region Ctor
        static CacheDataSetManager() { _instance = new CacheDataSetManager(); }

        /// <summary>
        /// 对外不能创建类的实例
        /// </summary>
        private CacheDataSetManager()
        {
            string strategyName = "EntLib";

            switch (strategyName)
            {
                case "EntLib":
                    _cacheProvider = new EntLibCacheProvider();
                    break;
               
                default:
                    throw new ArgumentException("缓存持久化方法不正确，目前只支持EntLib和Redis");
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 获取<c>CacheManager</c>类型的单件（Singleton）实例。
        /// </summary>
        public static CacheDataSetManager Instance
        {
            get { return _instance; }
        }
        #endregion

        public void Add(string key, string valKey, object value)
        {
            _cacheProvider.Add(key, valKey, value);
        }

        public void Put(string key, string valKey, object value)
        {
            _cacheProvider.Add(key, valKey, value);
        }

        public object Get(string key, string valKey)
        {
           return _cacheProvider.Get(key,valKey);
        }

        public void Remove(string key)
        {
            _cacheProvider.Remove(key);
        }

        public void Remove(string key, string valKey)
        {
            _cacheProvider.Remove(key,valKey);
        }

        public bool Exists(string key)
        {
            return _cacheProvider.Exists(key);
        }

        public bool Exists(string key, string valKey)
        {
            return _cacheProvider.Exists(key,valKey);
        }
    }
}
