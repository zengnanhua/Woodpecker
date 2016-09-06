using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Messaging
{
    public interface IMessageManager
    {
        /// <summary>
        /// 发送消息 
        /// </summary>
        /// <param name="recipient">接收者</param>
        /// <param name="subject">主题</param>
        /// <param name="body">消息主体</param>
        /// <param name="serverVirtualPath">本参数可以没有，服务端模块级路径，只在xmpp中有意义</param>
        void Send(string recipient, string subject, string body, string serverVirtualPath = null);

        /// <summary>
        /// 发送消息 
        /// </summary>
        /// <param name="recipient">接收者集合</param>
        /// <param name="subject">主题</param>
        /// <param name="body">消息主体</param>
        /// <param name="serverVirtualPath">本参数可以没有，服务端模块级路径，只在xmpp中有意义</param>
        void Send(IEnumerable<string> recipients, string subject, string body, string serverVirtualPath = null);

        /// <summary>
        /// 发送消息 
        /// </summary>
        /// <param name="recipient">接收者集合</param>
        /// <param name="subject">主题</param>
        /// <param name="isAsync">是否异步</param>
        /// <param name="body">消息主体</param>
        /// <param name="serverVirtualPath">本参数可以没有，服务端模块级路径，只在xmpp中有意义</param>
        void Send(IEnumerable<string> recipients, string subject, string body, bool isAsync, string serverVirtualPath = null);

    }
}
