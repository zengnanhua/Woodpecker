/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/11 21:29:43

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

namespace NanHuaDDD.Messaging.Implements
{
    internal class RTXMessageManager : Singleton<RTXMessageManager>,IMessageManager
    {



        public void Send(string recipient, string subject, string body, string serverVirtualPath = null)
        {
            throw new NotImplementedException();
        }

        public void Send(IEnumerable<string> recipients, string subject, string body, string serverVirtualPath = null)
        {
            throw new NotImplementedException();
        }

        public void Send(IEnumerable<string> recipients, string subject, string body, bool isAsync, string serverVirtualPath = null)
        {
            throw new NotImplementedException();
        }
    }
}
