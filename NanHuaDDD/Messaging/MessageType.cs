/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 20:15:53

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
    public enum MessageType
    {
        /// <summary>
        /// 电子邮件
        /// </summary>
        Email=1,
        /// <summary>
        /// 短信息
        /// </summary>
        SMS=2,
        /// <summary>
        /// RTX实时通讯
        /// </summary>
        RTX=3,
        /// <summary>
        /// XMPP消息推送
        /// </summary>
        XMPP=4
    }
}
