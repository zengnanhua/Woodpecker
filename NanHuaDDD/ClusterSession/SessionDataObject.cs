/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/5/13 17:29:05

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanHuaDDD.ClusterSession
{
    [Serializable]
    public class SessionDataObject
    {
        public byte[] Content { get; set; }
        public bool Locked { get; set; }
        public DateTime SetTime { get; set; }
        public int LockId { get; set; }
        public int ActionFlag { get; set; }
        public TimeSpan LockAge { get; set; }
    }
}
