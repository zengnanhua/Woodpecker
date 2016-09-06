/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/10 20:41:25

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Caching
{
    public class RuntimeCache:ICache
    {
        readonly static System.Web.Caching.Cache httpRuntimeCache = System.Web.HttpRuntime.Cache;
        readonly static int _expireMinutes = 20;  //ConfigConstants.ConfigManager.Config.Cache_ExpireMinutes;
        public void Put(string key, object obj)
        {
            httpRuntimeCache.Insert(key,obj);
        }

        public void Put(string key, object obj, int expireMinutes)
        {
            httpRuntimeCache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(expireMinutes));
        }

        public object Get(string key)
        {
            return httpRuntimeCache.Get(key);
        }

        public void Delete(string key)
        {
             httpRuntimeCache.Remove(key);
        }
    }
}
