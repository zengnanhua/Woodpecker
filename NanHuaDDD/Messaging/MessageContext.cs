/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 20:18:38

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Messaging
{
    /// <summary>
    /// 消息实体
    /// </summary>
    public class MessageEntity
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType Type { get; set; }
        /// <summary>
        /// 消息头
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 消息正文
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 接受方地址列表
        /// </summary>
        public IEnumerable<string> Addresses { get; set; }
        /// <summary>
        /// 是否处于准备发送状态
        /// </summary>
        public bool MessagePrepared { get; set; }

        public MessageEntity()
        {
            Addresses = Enumerable.Empty<string>();//这时Addresses!=null,使用Addresses.ToList().ForEach(i => Console.WriteLine(i));不会引发异常
        }
    }
}
