/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/10 20:34:31

*创建人：曾南华 

*版本号： V1.0.0.0

*描述: 配置文件缓存
*************************************************************************/


using NanHuaDDD.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NanHuaDDD.CacheConfigFile
{
    public class ConfigFileCache
    {
        private static ICache iCache = new RuntimeCache(); 
        public static object GetCache(string cacheKey)
        {
            return iCache.Get(cacheKey);
        }

        public static void SetCache(string CacheKey, object objObject)
        {
             iCache.Put(CacheKey, objObject);
        }

        public static void SetCache(string CacheKey, object objObject, int expireMinutes)
        {
            iCache.Put(CacheKey,objObject,expireMinutes);
        }

        public static void RemoveCache(string CacheKey)
        {
            iCache.Delete(CacheKey);
        }


    }
}
