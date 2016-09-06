/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 20:21:36

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using NanHuaDDD.Messaging.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.Messaging
{
    public sealed  class MessageFactory
    {
        public static IMessageManager GetService(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Email:
                    return EmailMessageManager.Instance;
                case MessageType.SMS:
                    return SMSMessageManager.Instance;
                default:
                    throw new NotImplementedException("消息生产者未被识别...");
            }
        }
    }
}
