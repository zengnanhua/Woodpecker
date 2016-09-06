/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/10 22:29:22

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Caching
{
    /// <summary>
    /// 缓存管理者
    /// </summary>
   public class CacheManager :Singleton<CacheManager>,ICache
    {
        #region 私有变量
        private static string _cacheProvider = ConfigConstants.FrameConfigManager.Config.Cache_Provider ?? "runtimecache";
        private ICache _iCache;
      
       
        #endregion

        #region 构造方法
        /// <summary>
        /// 类构造方法，对外不支持创建它的实例对象
        /// </summary>
        static CacheManager() { }
        private CacheManager()
        {
            switch (_cacheProvider.ToLower())
            { 
                case"runtimecache":
                    _iCache = new RuntimeCache();
                    break;
                case "memcachecache":
                    _iCache = new MemcacheCache();
                    break;
                default:
                    throw new ArgumentException("缓存提供者只支持RunTimeCache和RedisCache");
            }
        }
        #endregion 


       
    
        public void Put(string key, object obj)
        {
            _iCache.Put(key, obj);
        }

        public void Put(string key, object obj, int expireMinutes)
        {
            _iCache.Put(key,obj,expireMinutes);
        }

        public object Get(string key)
        {
            return _iCache.Get(key);
        }

        public void Delete(string key)
        {
            _iCache.Delete(key);
        }
    }
}
