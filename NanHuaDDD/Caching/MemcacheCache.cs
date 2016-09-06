/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 14:34:46

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Caching
{
    public class MemcacheCache:ICache
    {
        private static readonly MemcachedClient m_memcachedClient;
        private static string m_memcacheStr = ConfigConstants.FrameConfigManager.Config.MemcacheStr ?? "127.0.0.1:11211";
        static MemcacheCache()
        {

            string[] servers = m_memcacheStr.Split(',');
            try
            {
                //初始化池
                SockIOPool pool = SockIOPool.GetInstance();
                pool.SetServers(servers);
                pool.InitConnections = 3;
                pool.MinConnections = 3;
                pool.MaxConnections = 5;
                pool.SocketConnectTimeout = 1000;
                pool.SocketTimeout = 3000;
                pool.MaintenanceSleep = 30;
                pool.Failover = true;
                pool.Nagle = false;
                pool.Initialize();
                m_memcachedClient = new Memcached.ClientLibrary.MemcachedClient();
                m_memcachedClient.EnableCompression = false;
            }
            catch (Exception ex)
            {
                int i = 0;
            }
        }

        public void Put(string key, object obj)
        {
            m_memcachedClient.Set(key, obj);
        }

        public void Put(string key, object obj, int expireMinutes)
        {
            m_memcachedClient.Set(key, obj, DateTime.Now.AddMinutes(expireMinutes));
        }

        public object Get(string key)
        {
            return m_memcachedClient.Get(key);
        }

        public void Delete(string key)
        {
            m_memcachedClient.Delete(key);
        }
    }
}
