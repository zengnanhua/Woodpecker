using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web.Common
{
    public class HttpRuntimeCacheWriter : ICacheWriter
    {
        public void Set(string key, object value, DateTime exp)
        {
            if (HttpRuntime.Cache.Get(key) != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
            HttpRuntime.Cache.Insert(key, value, null, exp, TimeSpan.Zero);
        }

        public void Set(string key, object value)
        {
            if (HttpRuntime.Cache.Get(key) != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
            HttpRuntime.Cache.Insert(key, value, null, DateTime.MaxValue, TimeSpan.Zero);
        }

        public object Get(string key)
        {
            return HttpRuntime.Cache[key];
        }


        public void Delete(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }
    }
}
