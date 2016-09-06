/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 17:29:58

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.Caching;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace NanHuaDDD.ClusterSession
{

    /************************************************
     * 
     *   <sessionState mode="Custom" customProvider="SessionProvider" >
      <providers>
        <add name="SessionProvider" type="NanHuaDDD.ClusterSession.ClusterSessionStoreProvider,NanHuaDDD" timeout="1" accessKey="你好" />
      </providers>
     * 
     * ******************************************************************/

    /// <summary>
    /// 分布式session
    /// </summary>

    public class ClusterSessionStoreProvider : SessionStateStoreProviderBase
    {
        private CacheManager _Client = CacheManager.Instance;

        private static readonly int _DefaultSessionExpireMinute = 20;
        private int _timeout;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ClusterSessionStoreProvider()
        {
        }
        /// <summary>
        /// 请求初始化的时候
        /// </summary>
        /// <param name="context"></param>
        public override void InitializeRequest(HttpContext context)
        {

        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            // string applicationVirtualPath = AppDomain.CurrentDomain.BaseDirectory;


            if (string.IsNullOrWhiteSpace(config["timeout"]))
            {
                this._timeout = _DefaultSessionExpireMinute;
            }
            else
            {
                this._timeout = Convert.ToInt32(config["timeout"]);
            }
        }
        public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
        {
            return new SessionStateStoreData(new SessionStateItemCollection()
                , SessionStateUtility.GetSessionStaticObjects(context), timeout);
        }

        /// <summary>
        ///将新的会话状态添加到数据区中
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="timeout"></param>
        public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
        {
            SessionDataObject sessionObject = new SessionDataObject
            {
                Content = null,
                Locked = false,
                SetTime = DateTime.Now,
                LockId = 0,
                ActionFlag = 1
            };
            _Client.Put(id, sessionObject, timeout);
        }

        public override void Dispose()
        {
            //调用dispose 需要的操作
        }

        public override void EndRequest(HttpContext context)
        {
            //请求结束时调用
        }
        public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            SessionStateStoreData sessionStateStoreDate = null;
            SessionDataObject memcachedSessionObject = null;
            DateTime setTime = DateTime.Now;

            lockAge = TimeSpan.Zero;
            lockId = null;
            locked = false;
            actions = SessionStateActions.None;
            memcachedSessionObject = _Client.Get(id) as SessionDataObject;

            if (memcachedSessionObject != null)
            {
                //如果已经锁定
                if (memcachedSessionObject.Locked)
                {
                    lockAge = memcachedSessionObject.LockAge;
                    lockId = memcachedSessionObject.LockId;
                    locked = memcachedSessionObject.Locked;
                    actions = (SessionStateActions)memcachedSessionObject.ActionFlag;
                    return sessionStateStoreDate;
                }

                memcachedSessionObject.LockId++;
                memcachedSessionObject.SetTime = setTime;
                _Client.Put(id, memcachedSessionObject);


                actions = (SessionStateActions)memcachedSessionObject.ActionFlag;
                lockId = memcachedSessionObject.LockId;
                lockAge = memcachedSessionObject.LockAge;

                if (actions == SessionStateActions.InitializeItem)
                {
                    sessionStateStoreDate = this.CreateNewStoreData(context, _timeout);
                }
                else
                {
                    sessionStateStoreDate = this.Deserialize(context, memcachedSessionObject.Content, _timeout);
                }
                return sessionStateStoreDate;
            }
            return sessionStateStoreDate;
        }

        //从缓冲区中读取只读属性
        public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            return GetItem(context, id, out locked, out lockAge, out lockId, out actions);
        }

        //释放锁定
        public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
        {
            SessionDataObject memcachedSessionObject = _Client.Get(id) as SessionDataObject;
            if (memcachedSessionObject != null)
            {
                memcachedSessionObject.Locked = false;
                memcachedSessionObject.LockId = (Int32)lockId;
                _Client.Put(id, memcachedSessionObject, _timeout);
            }
        }
        //删除缓存数据
        public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
        {
            _Client.Delete(id);
        }

        public override void ResetItemTimeout(HttpContext context, string id)
        {
            object obj = _Client.Get(id);
            if (obj != null)
            {
                _Client.Put(id, obj, _timeout);
            }
        }
        //更新值
        public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
        {
            DateTime setTime = DateTime.Now;
            byte[] bytes = this.Serialize((SessionStateItemCollection)item.Items);
            SessionDataObject memcachedSessionObject = new SessionDataObject()
            {
                LockId = 0,
                Locked = false,
                Content = bytes,
                ActionFlag = 0,
                SetTime = setTime
            };
            _Client.Put(id, memcachedSessionObject, item.Timeout);
        }

        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            return false;
        }



        private SessionStateStoreData Deserialize(HttpContext context, byte[] bytes, int timeout)
        {
            MemoryStream stream = new MemoryStream(bytes);

            SessionStateItemCollection collection = new SessionStateItemCollection();
            if (stream.Length > 0)
            {
                BinaryReader reader = new BinaryReader(stream);
                collection = SessionStateItemCollection.Deserialize(reader);
            }
            return new SessionStateStoreData(collection, SessionStateUtility.GetSessionStaticObjects(context), timeout);
        }

        private byte[] Serialize(SessionStateItemCollection items)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(ms);
            if (items != null)
                items.Serialize(writer);

            writer.Close();
            return ms.ToArray();
        }




    }
}
