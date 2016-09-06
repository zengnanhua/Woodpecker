/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/17 10:40:22

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.CachingDataSet.Implements
{
    internal class EntLibCacheProvider:ICacheProvider
    {
        private readonly ICache _cacheManager = CacheManager.Instance;
        public void Add(string key, string valKey, object value)
        {
            Dictionary<string, object> dict = null;
            if (_cacheManager.Get(key) != null)
            {
                dict = (Dictionary<string, object>)_cacheManager.Get(key);
                dict[valKey] = value;
            }
            else {
                dict = new Dictionary<string, object>();
                dict.Add(valKey,value);
            }
            _cacheManager.Put(key,dict,20);
        }

        public void Put(string key, string valKey, object value)
        {
            Add(key, valKey, value);
        }

        public object Get(string key, string valKey)
        {
            if (_cacheManager.Get(key) != null)
            {
                Dictionary<string, object> dict = (Dictionary<string, object>)_cacheManager.Get(key);
                if (dict != null && dict.ContainsKey(valKey))
                {
                    return dict[valKey];
                }
                else {
                    return null;
                }
            }
            return null;
        }

        public void Remove(string key)
        {
            _cacheManager.Delete(key);
        }
        public void Remove(string key, string valKey)
        {
            if (Exists(key))
            {
                Dictionary<string, object> dic = (Dictionary<string, object>)_cacheManager.Get(key);
                if (dic.ContainsKey(valKey))
                {
                    dic.Remove(valKey);
                }
            }
        }

        public bool Exists(string key)
        {
            if (_cacheManager.Get(key) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Exists(string key, string valKey)
        {
            return Exists(key) &&
                ((Dictionary<string, object>)_cacheManager.Get(key)).ContainsKey(valKey);
        }


    }
}
